using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration; 
using Microsoft.IdentityModel.Tokens;
using AuthGambling.Dtos;
using AuthGambling.interfaces;
using AuthGambling.models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AuthGambling.Controllers
{ 
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            _config = config;
            _repo = repo;
        }
        
        public async Task<IActionResult> Index()
        {
 
            return Ok();
        }
     

        [HttpPost, Route("/user", Name = "user")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            userForRegisterDto.email = userForRegisterDto.email.ToLower();

            if (await _repo.UserExists( userForRegisterDto.email))
                return BadRequest("Username already exists");

            var userToCreate = new customers
            {
                email = userForRegisterDto.email,
                firstName= userForRegisterDto.firstName,
                lastName= userForRegisterDto.lastName,
                marketingConsent= userForRegisterDto.marketingConsent
            };

            var createdUser = await _repo.Register(userToCreate, _config.GetSection("AppSettings:Token").Value);
            var claims = new[]
            {
                    new Claim(ClaimTypes.NameIdentifier, userToCreate.Id.ToString()),
                    new Claim(ClaimTypes.Name, userToCreate.email)
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);
 
            return Ok(new { Id = userToCreate.Id, accessToken = tokenHandler.WriteToken(token) });
        }


        [HttpGet("/user/{id}")]
        public async Task<IActionResult> getUser(string id)
        {
            
            var user = await _repo.getUser(id);
            if (user == null)
                return BadRequest();
            return Ok(user);

        }

    }
}

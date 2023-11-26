using Microsoft.EntityFrameworkCore;
using AuthGambling.interfaces;
using AuthGambling.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace AuthGambling.repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly db_tables _context;
        public AuthRepository(db_tables context)
        {
            _context = context;
        }

        public async Task<customers> getUser(string Id)
        {
            
           return await _context.customers.SingleOrDefaultAsync(a => a.Id == Id);
             
        }

        public async Task<customers> Register(customers user, string password)
        {
            byte[] passwordHash, passwordSalt;
           user.Id= CreateEmailHash(user.email+password);

            await _context.customers.AddAsync(user);

            await _context.SaveChangesAsync();

            return user;
        }

        private string CreateEmailHash(string source)
        {
       
            using (SHA1 sha1Hash = SHA1.Create())
            {
                //From String to byte array
                byte[] sourceBytes = Encoding.UTF8.GetBytes(source);
                byte[] hashBytes = sha1Hash.ComputeHash(sourceBytes);
                var sb = new StringBuilder();

                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.customers.AnyAsync(x => x.email == username))
                return true;

            return false;
        }
    }
}

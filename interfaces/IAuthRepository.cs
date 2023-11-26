using AuthGambling.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthGambling.interfaces
{
    public interface IAuthRepository
    {
        Task<customers> Register(customers user, string password);

        Task<bool> UserExists(string username);
        Task<customers> getUser(string Id);
    }
}

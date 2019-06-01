using IdentityApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApi.Services
{
    public interface IAuthService
    {
        Task RegisterUser(User model);
        Task<User> AuthenticateUser(User model);
        Task<User> GetUserById(string _id);
        Task UpdateUserInfo(string _id, User model);
    }
}

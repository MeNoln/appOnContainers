using IdentityApi.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApi.Services
{
    public interface IAuthService
    {
        Task<User> RegisterUser(User model);
        Task<User> AuthenticateUser(User model);
        Task<User> FindUserById(string _id);
        Task UpdateUserInfo(string _id, User model);
        Task<byte[]> GetImage(string id);
        Task<byte[]> SaveImage(string id, IFormFile imageStream);
        Task SetDefaultImage(User model);
    }
}

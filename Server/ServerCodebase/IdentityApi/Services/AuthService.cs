using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityApi.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace IdentityApi.Services
{
    public class AuthService : IAuthService
    {
        private IMongoCollection<User> db;
        public AuthService(IConfiguration cfg)
        {
            var client = new MongoClient(cfg.GetConnectionString("MongoDB"));
            var database = client.GetDatabase("authdb");
            db = database.GetCollection<User>("users");
        }

        public Task RegisterUser(User model)
        {
            throw new NotImplementedException();
        }

        public Task<User> AuthenticateUser(User model)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserById(string _id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserInfo(string _id, User model)
        {
            throw new NotImplementedException();
        }
    }
}

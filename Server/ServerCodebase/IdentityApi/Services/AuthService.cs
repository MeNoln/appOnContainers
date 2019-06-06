using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityApi.Models;
using IdentityApi.Services.EncodeUtil;
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

        public async Task<User> RegisterUser(User model)
        {
            await db.InsertOneAsync(model);
            var addedUser = await AuthenticateUser(model);

            return addedUser;
        }

        public async Task<User> AuthenticateUser(User model)
        {
            var user = await db.Find<User>(i => i.Login == model.Login && i.Password == model.Password).FirstOrDefaultAsync();
            if(user != null)
                user._id = CipherClass.Encipher(user._id);
            return user;
        }

        public async Task<User> AuthenticateUser(string _id)
        {
            _id = CipherClass.Decipher(_id);
            return await db.Find<User>(i => i._id == _id).FirstOrDefaultAsync();
        }

        public async Task UpdateUserInfo(string _id, User model)
        {
            await db.ReplaceOneAsync(i => i._id == _id, model);
        }
    }
}

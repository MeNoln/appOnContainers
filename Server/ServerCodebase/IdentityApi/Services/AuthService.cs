using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IdentityApi.Models;
using IdentityApi.Services.EncodeUtil;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace IdentityApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHostingEnvironment host; //To get folder destination
        private IMongoCollection<User> db;
        private IGridFSBucket gridFS; //File bucket
        public AuthService(IConfiguration cfg, IHostingEnvironment host)
        {
            var client = new MongoClient(cfg.GetConnectionString("MongoDB"));
            var database = client.GetDatabase("authdb");
            db = database.GetCollection<User>("users");

            gridFS = new GridFSBucket(database);
            this.host = host;
        }

        //Create new User
        public async Task<User> RegisterUser(User model)
        {
            var existsUser = await db.Find<User>(i => i.Login == model.Login).FirstOrDefaultAsync();
            if (existsUser != null)
                return new User { UserAge = -1};

            await db.InsertOneAsync(model); //Insert new data
            var addedUser = await AuthenticateUser(model); //Get this user
            await SetDefaultImage(addedUser);

            return addedUser;
        }

        //Authenticate User, if user not null Encipher id for cookie on client
        public async Task<User> AuthenticateUser(User model)
        {
            var user = await db.Find<User>(i => i.Login == model.Login && i.Password == model.Password).FirstOrDefaultAsync();
            if(user != null)
                user._id = CipherClass.Encipher(user._id);
            return user;
        }

        //Find User by Decipherd Id
        public async Task<User> FindUserById(string _id)
        {
            _id = CipherClass.Decipher(_id);
            var user = await db.Find<User>(i => i._id == _id).FirstOrDefaultAsync();
            if (user != null)
                user._id = CipherClass.Encipher(user._id);

            return user;
        }

        //Update information about User
        public async Task<User> UpdateUserInfo(User model)
        {
            model._id = CipherClass.Decipher(model._id);
            var filter = Builders<User>.Filter.Eq("_id", new ObjectId(model._id));
            var updateFilter = Builders<User>.Update.Set("Login", model.Login).Set("UserName", model.UserName).Set("UserAge", model.UserAge);
            await db.UpdateOneAsync(filter, updateFilter);

            //Get just updated user and cipher his Id
            var user = await db.Find<User>(i => i._id == model._id).FirstOrDefaultAsync();
            if (user != null)
                user._id = CipherClass.Encipher(user._id);
            return user;
        }

        //Get User Image
        public async Task<byte[]> GetImage(string id)
        {
            User user = await FindUserById(id);
            return await gridFS.DownloadAsBytesAsync(new ObjectId(user.ImageId));
        }

        //Save Uploaded image
        public async Task<byte[]> SaveImage(string id, IFormFile imageStream)
        {
            //If User Already has image => delete it
            User user = await FindUserById(id);
            if (!String.IsNullOrWhiteSpace(user.ImageId))
                await gridFS.DeleteAsync(new ObjectId(user.ImageId));

            //Open FileStream and save Image in GridFSBucket
            var options = new GridFSUploadOptions { Metadata = new BsonDocument("contentType", imageStream.ContentType) };
            using (var reader = new StreamReader((Stream)imageStream.OpenReadStream()))
            {
                var stream = reader.BaseStream;
                var fileId = await gridFS.UploadFromStreamAsync(imageStream.FileName, stream, options);
                user.ImageId = fileId.ToString();
            }

            //Update User ImageId filed
            var filter = Builders<User>.Filter.Eq("_id", new ObjectId(user._id));
            var updateFilter = Builders<User>.Update.Set("ImageId", user.ImageId);
            await db.UpdateOneAsync(filter, updateFilter);

            return await gridFS.DownloadAsBytesAsync(new ObjectId(user.ImageId));
        }

        //Set Default Image when user registers
        public async Task SetDefaultImage(User model)
        {
            //If User Already has image => delete it
            if (!String.IsNullOrWhiteSpace(model.ImageId))
                await gridFS.DeleteAsync(new ObjectId(model.ImageId));

            //Take default image that stores on server
            var filePath = Path.Combine(host.WebRootPath, "images", "default.png");
            var defaultImage = File.OpenRead(filePath);

            //Open FileStream and save Image in GridFSBucket
            var options = new GridFSUploadOptions { Metadata = new BsonDocument("contentType", "image/png") };
            using (var reader = new StreamReader((Stream)defaultImage))
            {
                var stream = reader.BaseStream;
                var fileId = await gridFS.UploadFromStreamAsync("default", stream, options);
                model.ImageId = fileId.ToString();
            }

            //Update User ImageId filed
            var userId = CipherClass.Decipher(model._id);
            var filter = Builders<User>.Filter.Eq("_id", new ObjectId(userId));
            var updateFilter = Builders<User>.Update.Set("ImageId", model.ImageId);
            await db.UpdateOneAsync(filter, updateFilter);
        }
    }
}
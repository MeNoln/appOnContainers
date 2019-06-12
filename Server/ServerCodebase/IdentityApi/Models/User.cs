using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApi.Models
{
    public class User
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        [BsonElement("Login")]
        public string Login { get; set; }
        [BsonElement("Password")]
        public string Password { get; set; }
        [BsonElement("UserName")]
        public string UserName { get; set; }
        [BsonElement("UserAge")]
        public int UserAge { get; set; }
        [BsonElement("ImageId")]
        public string ImageId { get; set; }
    }
}

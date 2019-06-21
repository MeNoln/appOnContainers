using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityApi.Models;
using IdentityApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private IAuthService db;
        public AuthController(IAuthService db)
        {
            this.db = db;
        }

        //POST: api/auth/reg
        [HttpPost, Route("reg")]
        public async Task<IActionResult> Register(User model)
        {
            //If user entered already exists login return message
            var user = await db.RegisterUser(model);
            if (user.UserAge == -1)
                return Json("exist");

            return Json(user);
        }

        //GET: api/auth/log
        [HttpGet, Route("log")]
        public async Task<IActionResult> AuthenticateUserByModel([FromQuery]User model)
        {
            var user = await db.AuthenticateUser(model);
            if (user == null)
                return Json(null);

            return Json(user);
        }

        //GET: api/auth/find/{_id}
        [HttpGet, Route("find/{_id}")]
        public async Task<IActionResult> FindUser(string _id)
        {
            var user = await db.FindUserById(_id);
            if (user == null)
                return NotFound();

            return Json(user);
        }

        //PUT: api/auth/update
        [HttpPut, Route("update")]
        public async Task<IActionResult> UpdateInfo(User model)
        {
            var user = await db.UpdateUserInfo(model);
            if (user == null)
                return NotFound();

            return Json(user);
        }

        //GET: api/auth/img/{id}
        [HttpGet, Route("img/{id}")]
        public async Task<IActionResult> GetImage(string id)
        {
            var image = await db.GetImage(id);
            if (image == null)
                return NotFound();
            
            return File(image, "image/png");
        }

        //POST: api/auth/addimg
        [HttpPost, Route("addimg")]
        public async Task<IActionResult> AddImage([FromForm] string id, [FromForm] IFormFile uploadedImage)
        {
            if (uploadedImage == null)
                return BadRequest();
            if (!uploadedImage.ContentType.Contains("image"))
                return Ok("Not an Image");

            var image = await db.SaveImage(id, uploadedImage);
            return File(image, "image/png");
        }
    }
}
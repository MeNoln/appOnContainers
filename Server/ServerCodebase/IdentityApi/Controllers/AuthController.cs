﻿using System;
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

        [HttpPost, Route("reg")]
        public async Task<IActionResult> Register(User model)
        {
            var user = await db.RegisterUser(model);

            Response.Cookies.Append("authCook", user._id, new CookieOptions { Expires = DateTime.Now.AddDays(2) });

            return Json(user);
        }

        [HttpGet, Route("log")]
        public async Task<IActionResult> AuthenticateUserByModel(User model)
        {
            var user = await db.AuthenticateUser(model);
            if (user == null)
                return NotFound();

            if(!Request.Cookies.ContainsKey("authCook"))
                Response.Cookies.Append("authCook", user._id, new CookieOptions { Expires = DateTime.Now.AddDays(2) });

            return Json(user);
        }

        [HttpGet, Route("find/{_id}")]
        public async Task<IActionResult> FindUserById(string _id)
        {
            var user = await db.AuthenticateUser(_id);
            if (user == null)
                return NotFound();

            return Json(user);
        }
    }
}
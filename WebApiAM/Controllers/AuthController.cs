﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiAM.Services;
using WebApiAM.Models;
using WebApiAM.Helpers;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace WebApiAM.Controllers
{   
    [Authorize]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        const int admin = 1; 
        const int user = 2; 

        private readonly IUserService userService;
        private readonly IConfiguration config;

        public AuthController(IUserService userService, IConfiguration config)
        {
            this.userService = userService;
            this.config = config;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Create([FromBody] RegisterUserViewModel model)
        {
            model.RoleId = user;
            try
            {
                userService.Create((User)model, model.Password);
                return Ok();
            }
            catch (AppException e)
            {
                return BadRequest(new
                {
                    message = e.Message
                });
            }
            catch
            {
                return BadRequest(new
                {
                    message = "Произошла ошибка, попробуйте позже"
                });
            }
        }        
        [AllowAnonymous]
        [HttpPost("Auth")]
        public IActionResult Authenticate([FromBody] AuthUserViewModel model) {
            if(model == null || model.Email == null || model.Password == null)
                return BadRequest(new { message = "Электронная почта или пароль не валидны" });

            var user = userService.Authenticate(model.Email,model.Password);

            if (user == null)
                return BadRequest(new { message = "Электронная почта или пароль не валидны" });
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(config.GetSection("Config:SecretKey").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.RoleId.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new {
                token = tokenString,
                email = user.Email,
                fullName = user.FullName,
                phone = user.Phone
            });
        }
    }
}
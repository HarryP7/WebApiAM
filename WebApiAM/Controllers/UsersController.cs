﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiAM.Models;

namespace WebApiAM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        AppDbContext db;
        public UsersController(AppDbContext context)
        {
            this.db = context;
            if (!db.Users.Any())
            {
               db.Roles.Add(new Role { Name = "admin" });
               db.Roles.Add(new Role { Name = "user" });
               db.Users.Add(new User { FullName = "Tom Riddle Marvolo", Login = "Lord VolnDeMort", Email = "user@gmail.com", RoleId = 2, Phone = "9 999 999 9999"});
               db.Users.Add(new User { FullName = "Darth Vader (Sith)", Login = "Darth Vader", Email = "user2@gmail.com", RoleId = 2, Phone = "8 888 888 8888" });
               db.Users.Add(new User { FullName = "Thanos", Login = "Безумный Титан", Email = "admin@gmail.com", RoleId = 1, Phone = "0000" });
                db.SaveChanges();
            }
        }
        // GET: api/users
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return db.Users.ToList();
        }

        // GET: api/users/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            User user = db.Users.FirstOrDefault(p => p.Id == id);
            if (user == null)
                return NotFound();
            return new ObjectResult(user);
        }

        // POST: api/users
        [HttpPost]
        public IActionResult Post([FromBody]User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            db.Users.Add(user);
            db.SaveChanges();
            return Ok(user);
        }

        // PUT: api/users/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]User user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            if (!db.Users.Any(x => x.Id == user.Id))
            {
                return NotFound();
            }

            db.Update(user);
            db.SaveChanges();
            return Ok(user);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            User user = db.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            db.Users.Remove(user);
            db.SaveChanges();
            return Ok(user);
        }
    }
}

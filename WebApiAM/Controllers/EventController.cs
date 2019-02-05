using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApiAM.Helpers;
using WebApiAM.Models;
using WebApiAM.Repository;

namespace WebApiAM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : Controller
    {
        private readonly AppDbContext db;

        public EventController(AppDbContext db)
        {
            this.db = db;
            if (!db.Events.Any())
            {
                db.Events.Add(new Event { EvDate = new DateTime(2015, 7, 20), Cost = 100, Status = EventStatus.Create, Fk_user = 1, Comment = "", Service = new Service { Lat = 56.129042M, Lng = 40.40703M, DatePlace = DateTime.Now, Title = "Hunt for unicorns" } });
                db.SaveChanges();
            }
        }
        public IEnumerable<Event> Fetch(Expression<Func<Event, bool>> predicate)
        {
            return (db.Events.Where(predicate).Include(o => o.Service)
                .Include(o => o.User) as IEnumerable<Event>).Select(o =>
                {
                    o.User.PasswordHash = null;
                    o.User.PasswordSalt = null;
                    return o;
                });
        }
        // GET: api/Event
        //[Authorize(Roles = "admin,user")]
        [HttpGet]
        public IEnumerable<Event> GetEvents()
        {
            return db.Events.Include(p => p.Service).ToList();
        }
        //// GET: api/Event
        //[Authorize(Roles = "admin,user")]
        //[HttpGet("getevents")]
        //public IActionResult GetEvents()
        //{
        //    try
        //    {
        //        IEnumerable<Event> events;
        //        if (User.IsInRole("user"))
        //        {
        //            var userId = int.Parse(User.Identity.Name);
        //            events = Fetch(ev => ev.Fk_user == userId);
        //        }
        //        else
        //            events = Fetch(o => true);
        //
        //        return Ok(new { events });
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(new
        //        {
        //            message = "На сервере произошла ошибка, попробуйте позже"
        //        });
        //    }
        //}
        // GET: api/Event/5
        [HttpGet("{id}", Name = "GetEvent")]
        public IActionResult GetEvent(int id)
        {
            Event ev = db.Events.Include(p => p.Service).FirstOrDefault(p => p.Id == id);
            if (ev == null)
                return NotFound();
            return new ObjectResult(ev);
        }
        // POST: api/Event
        [Authorize(Roles = "user")]
        [HttpPost("createevent")]
        public IActionResult CreateEvent([FromBody] EventViewModel model)
        {
            try
            {
                var Event = (Event)model;
                Event.Fk_user = int.Parse(User.Identity.Name);
                Event.Service.DatePlace = DateTime.Now;

                db.Events.Add(Event);
                db.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    message = "На сервере произошла ошибка, попробуйте позже"
                });
            }
        }
        // PUT: api/Status/5
        [Authorize(Roles = "admin")]
        [HttpPut("setstatus/{id}")]
        public IActionResult SetStatus([FromRoute] int id, [FromQuery] int status)
        {
            try
            {
                var ev = db.Events.FirstOrDefault(o => o.Id == id);
                if (ev != null)
                {
                    ev.Status = (EventStatus)status;
                    db.SaveChanges();
                    return Ok();
                }
                else
                    return BadRequest("Событие не найдено");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // PUT: api/Event/5
        [HttpPut("{id}"), Authorize(Roles = "user, admin")]
        public IActionResult Put(int id, [FromBody] Event ev)
        {
            if (ev == null)
            {
                return BadRequest();
            }
            if (!db.Users.Any(x => x.Id == ev.Id))
            {
                return NotFound();
            }

            db.Update(ev);
            db.SaveChanges();
            return Ok(ev);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Event ev = db.Events.FirstOrDefault(x => x.Id == id);
            if (ev == null)
            {
                return NotFound();
            }
            db.Events.Remove(ev);
            db.SaveChanges();
            return Ok(ev);
        }
    }
}

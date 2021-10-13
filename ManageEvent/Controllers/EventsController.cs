using ManageEvent.Dao;
using ManageEvent.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ManageEvent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        public EventsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        IConfiguration _configuration;
        // GET: api/<EventsController>
        [HttpGet("{id}/{token}")]
        public IEnumerable<Event> Get(int id, string token)
        {
            try
            {
                if (new UserDao().Authentication(token))
                {
                    return new EventDao().GetEventList(id, "").ToArray();
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }

        }




        // POST api/<EventsController>
        [HttpPost]
        public void Post([FromBody] EventForPost eventForPost)
        {
            if (new UserDao().Authentication(eventForPost.Token))
            {
                Event newEvent = new Event();
                newEvent.Name = eventForPost.Name;
                newEvent.Description = eventForPost.Description;
                newEvent.UserId = eventForPost.UserId;
                newEvent.EventDateAt = eventForPost.EventDateAt;
                new EventDao().Create(newEvent);
            }
        }

        [HttpPost("mail/")]
        public void Post([FromBody] Email email)
        {
            if (new UserDao().Authentication(email.Token))
            {
                new EventDao().SendEmail(email);
            }
        }


        // PUT api/<EventsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] EventForPut eventForPut)
        {
            if (new UserDao().Authentication(eventForPut.Token))
            {
                Event newEvent = new Event();
                newEvent.Name = eventForPut.Name;
                newEvent.Description = eventForPut.Description;
                newEvent.EventDateAt = eventForPut.EventDateAt;
                newEvent.Id = id;
                new EventDao().Update(newEvent);
            }
        }

        // DELETE api/<EventsController>/5
        [HttpDelete("{id}/{token}")]
        public void Delete(int id, string token)
        {
            if (new UserDao().Authentication(token))
            {
                new EventDao().DeleteById(id);
            }
        }
    }
}
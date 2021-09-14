using ManageEvent.Dao;
using ManageEvent.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ManageEvent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventAttendeesController : ControllerBase
    {
        IConfiguration _configuration;
        public EventAttendeesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // GET: api/<EventAttendeesController[]>
        [HttpGet("{id}/{token}")]
        public IEnumerable<EventAttendees> Get(int id, string token)
        {
            try
            {
                if (new UserDao().Authentication(token))
                {
                    return new EventAttendeesDao().GetAllEventAttendees(id).ToArray();
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
        // POST api/<EventAttendeesController>
        [HttpPost]
        public void Post([FromBody] EventAttendeesForPost eventAttendeesForPost)
        {
            if (new UserDao().Authentication(eventAttendeesForPost.Token))
            {
                EventAttendees eventAttendees = new EventAttendees();
                eventAttendees.GroupId = eventAttendeesForPost.GroupId;
                eventAttendees.Name = eventAttendeesForPost.Name;
                eventAttendees.Email = eventAttendeesForPost.Email;
                eventAttendees.Other = eventAttendeesForPost.Other;
                new EventAttendeesDao().Create(eventAttendees);
            }
        }

        // PUT api/<EventAttendeesController>/id
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] EventAttendeesForPut eventAttendeesForPut)
        {
            if (new UserDao().Authentication(eventAttendeesForPut.Token))
            {
                EventAttendees eventAttendees = new EventAttendees();
                eventAttendees.Name = eventAttendeesForPut.Name;
                eventAttendees.Email = eventAttendeesForPut.Email;
                eventAttendees.Other = eventAttendeesForPut.Other;
                eventAttendees.Id = id;
                new EventAttendeesDao().Update(eventAttendees);
            }
        }

        // DELETE api/<EventAttendeesController>/id
        [HttpDelete("{id}/{token}")]
        public void Delete(int id, string token)
        {
            if (new UserDao().Authentication(token))
            {
                new EventAttendeesDao().DeleteById(id);
            }
        }
    }
}

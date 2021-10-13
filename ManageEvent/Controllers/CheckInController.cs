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
    public class CheckInController : ControllerBase
    {
        IConfiguration _configuration;
        public CheckInController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // GET: api/<CheckInController[]>
        [HttpGet("{id}/{token}")]
        public IEnumerable<CheckIn> Get(int id, string token)
        {
            try
            {
                if (new UserDao().Authentication(token))
                {
                    return new CheckInDao().GetAll(id).ToArray();
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

        [HttpPut("checkInQR/{id}")]
        public void Put(int id,[FromBody] string token)
        {
            if (new UserDao().Authentication(token))
            {
                new CheckInDao().CheckIn(id);
            }
        }


        [HttpPost]
        public void Post([FromBody] CheckInForPost checkInForPost)
        {
            if (new UserDao().Authentication(checkInForPost.Token))
            {
                CheckIn checkIn = new CheckIn();
                checkIn.EventId = checkInForPost.EventId;
                checkIn.Name = checkInForPost.Name;
                checkIn.Email = checkInForPost.Email;
                checkIn.Other = checkInForPost.Other;
                new CheckInDao().Create(checkIn);
            }
        }

        // POST api/<CheckInController>
        [HttpPost("copyFromAttendees")]
        public void Post([FromBody] CheckInForCopy checkInForCopy)
        {
            if (new UserDao().Authentication(checkInForCopy.Token))
            {
                var checkInList = new EventAttendeesDao().GetAllEventAttendees(checkInForCopy.GroupId);
                foreach(EventAttendees eventAttendees in checkInList)
                {
                    new CheckInDao().Create(eventAttendees, checkInForCopy.EventId);
                }
            }
        }

        // PUT api/<CheckInController>/id
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] CheckInForPut checkInForPut)
        {
            if (new UserDao().Authentication(checkInForPut.Token))
            {
                CheckIn checkIn = new CheckIn();
                checkIn.Name = checkInForPut.Name;
                checkIn.Email = checkInForPut.Email;
                checkIn.Other = checkInForPut.Other;
                checkIn.Id = id;
                new CheckInDao().Update(checkIn);
            }
        }


        // DELETE api/<CheckInController>/id
        [HttpDelete("{id}/{token}")]
        public void Delete(int id, string token)
        {
            if (new UserDao().Authentication(token))
            {
                new CheckInDao().DeleteById(id);
            }
        }
    }
}

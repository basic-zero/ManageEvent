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
        [HttpGet("{id}")]
        public IEnumerable<Event> Get(string id)
        {
            return new EventDao().getEventList(id, "").ToArray();
        }


        // POST api/<EventsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<EventsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EventsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

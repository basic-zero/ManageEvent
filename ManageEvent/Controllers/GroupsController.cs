using ManageEvent.Dao;
using ManageEvent.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ManageEvent.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase {

        public GroupsController(IConfiguration configuration) {
            _configuration = configuration;
        }
        IConfiguration _configuration;


        // GET api/<GroupsController>/5
        [HttpGet("{id}/{token}")]
        public IEnumerable<Group> Get(int id,string token) {
            try {
                if (new UserDao().Authentication(token)) {
                    return new GroupDao().getGroupList(id, "").ToArray();
                } else {
                    return null;
                }
            } catch {
                return null;
            }

        }

        // POST api/<EventsController>
        [HttpPost]
        public void Post([FromBody] GroupForPost groupForPost) {
            if (new UserDao().Authentication(groupForPost.Token)) {
                Group newGroup = new Group();
                newGroup.Name = groupForPost.Name;
                newGroup.Description = groupForPost.Description;
                newGroup.UserId = groupForPost.UserId;
                new GroupDao().Create(newGroup);
            }
        }

        // PUT api/<EventsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] GroupForPut groupForPut) {
            if (new UserDao().Authentication(groupForPut.Token)) {
                Group newGroup = new Group();
                newGroup.Name = groupForPut.Name;
                newGroup.Description = groupForPut.Description;
                newGroup.Id = id;
                new GroupDao().Update(newGroup);
            }
        }

        // DELETE api/<EventsController>/5
        [HttpDelete("{id}/{token}")]
        public void Delete(int id, string token) {
            if (new UserDao().Authentication(token)) {
                new GroupDao().DeleteById(id);
            }
        }
    }
}

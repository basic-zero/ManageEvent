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
    public class UsersController : ControllerBase {
        public UsersController(IConfiguration configuration) {
            _configuration = configuration;
        }

        IConfiguration _configuration;

        // GET api/<UsersController>/5
        [HttpGet("{token}")]
        public bool Get(string token) {
            return new UserDao().Authentication(token);
        }

        // POST api/<UsersController>
        [HttpPost("register/")]
        public void Post([FromBody] UserRegister user) {
            User registerUser = new User();
            registerUser.Email = user.Email;
            registerUser.Name = user.Name;
            registerUser.Password = user.Password;
            registerUser.Type = true;
            new UserDao().RegisterWithEmailPwd(registerUser);
        }

        [HttpPost("login/")]
        public string Post([FromBody] UserLogin user) {
            string token = null;
            User loginUser = new User();
            loginUser.Email = user.Email;
            loginUser.Password = user.Password;
            loginUser.Type = true;
            token = new UserDao().LoginWithEmailPwd(loginUser);
            return token;
        }

        // PUT api/<UsersController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value) {
        //}

        // DELETE api/<UsersController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id) {
        //}
    }
}

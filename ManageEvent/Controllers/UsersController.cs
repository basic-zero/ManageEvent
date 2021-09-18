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

        // POST api/registerByEmailPwd
        // return 0 => "Error"
        // return 1 => "Ok"
        // return 2 => "Email đã tồn tại"
        [HttpPost("registerByEmailPwd/")]
        public int Post([FromBody] UserRegister user) {
            int result = 0;
            try {
                User registerUser = new User();
                registerUser.Email = user.Email;
                registerUser.Name = user.Name;
                registerUser.Password = user.Password;
                if (new UserDao().RegisterWithEmailPwd(registerUser)) {
                    result = 1;
                }
            } catch (Exception e) {
                if (e.Message.Contains("Cannot insert duplicate key in object 'dbo.tblUser'")) {
                    result = 2;
                }
            }
            return result;
        }

        // POST api/registerByGG
        // return 0 => "Error"
        // return 1 => "Ok"
        // return 2 => "Email đã được đăng ký"
        [HttpPost("registerByGG/")]
        public int Post([FromBody] UserRegisterByGG user) {
            int result = 0;
            try {
                User registerUser = new User();
                registerUser.Email = user.Email;
                registerUser.Name = user.Name;
                registerUser.Token = user.Token;
                if (new UserDao().RegisterWithGG(registerUser, registerUser.Token)) {
                    result = 1;
                }
            } catch (Exception e) {
                if (e.Message.Contains("Cannot insert duplicate key in object 'dbo.tblUser'")) {
                    result = 2;
                }
            }
            return result;

        }

        // POST api/loginByEmailPwd
        // return 0 => "Error"
        // return <token> => "Ok"
        // return 2 => "Email không tồn tại"
        // return 3 => "Sai loại account"
        // return 4 => "Sai mật khẩu"
        [HttpPost("loginByEmailPwd/")]
        public string Post([FromBody] UserLogin user) {

            string result = "0";
            try {
                User loginUser = new User();
                loginUser.Email = user.Email;
                loginUser.Password = user.Password;
                loginUser.Type = true;
                result = new UserDao().LoginWithEmailPwd(loginUser);
            } catch (Exception e) {
                if (e.Message.Contains("email does not exist")) {
                    result = "2";
                } else if (e.Message.Contains("incorrect type of account")) {
                    result = "3";
                } else if (e.Message.Contains("incorrect password")) {
                    result = "4";
                }
            }
            return result;
        }
        // return 0 => "Error"
        // return 1 => "Ok"
        // return 2 => "Email không tồn tại"
        // return 3 => "Sai loại account"
        [HttpPost("loginByGG/")]
        public int Post([FromBody] string token) {
            int result = 0;
            try {
                if (new UserDao().LoginWithGG(token)) {
                    result = 1;
                }
            } catch (Exception e) {
                if (e.Message.Contains("incorrect type of account")) {
                    result = 3;
                }
                if (e.Message.Contains("email does not exist")) {
                    result = 2;
                }
            }
            return result;
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

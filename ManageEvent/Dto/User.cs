using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageEvent.Dto {
    public class User {
        public User() {
        }

        public User(int id, string email, string name, string password, string token, bool status, bool type) {
            Id = id;
            Email = email;
            Name = name;
            Password = password;
            Token = token;
            Status = status;
            Type = type;
        }
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public bool Status { get; set; }
        public bool Type { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageEvent.Dto {
    public class CheckIn {
        public CheckIn() {
        }

        public CheckIn(int id, string name, string email, string other) {
            Id = id;
            Name = name;
            Email = email;
            Other = other;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Other { get; set; }

    }
}
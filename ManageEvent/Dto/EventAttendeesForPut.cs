using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageEvent.Dto
{
    public class EventAttendeesForPut
    {
        public EventAttendeesForPut(){}

        public EventAttendeesForPut(string name, string email, string other, string token)
        {
            Name = name;
            Email = email;
            Other = other;
            Token = token;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Other { get; set; }
        public string Token { get; set; }
    }
}

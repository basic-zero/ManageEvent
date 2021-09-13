using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageEvent.Dto
{
    public class EventAttendeesForPost
    {
        public EventAttendeesForPost() { }

        public EventAttendeesForPost(int groupId, string name, string email, string other, string token)
        {
            this.GroupId = groupId;
            this.Name = name;
            this.Email = email;
            this.Other = other;
            this.Token = token;
        }

        public int GroupId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Other { get; set; }
        public string Token { get; set; }
    }
}

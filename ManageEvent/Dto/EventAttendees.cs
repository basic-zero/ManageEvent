using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ManageEvent.Dto
{
    public class EventAttendees
    {
        public EventAttendees() {
        }

        public EventAttendees(int id, string name, string email, string other, int groupId, bool status)
        {
            Id = id;
            Name = name;
            Email = email;
            Other = other;
            GroupId = groupId;
            Status = status;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Other { get; set; }
        public int GroupId { get; set; }
        public bool Status { get; set; }

    }
}

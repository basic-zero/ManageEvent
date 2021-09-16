using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageEvent.Dto {
    public class Group {
        public Group() {
        }

        public Group(int id, string name, string description, bool status, List<EventAttendees> eventAttendees) {
            Id = id;
            Name = name;
            Description = description;
            Status = status;
            this.eventAttendees = eventAttendees;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public int UserId { get; set; }
        public List<EventAttendees> eventAttendees { get; set; }
    }



}

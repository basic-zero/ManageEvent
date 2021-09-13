﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageEvent.Dto {
    public class EventAttendees {
        public EventAttendees() {
        }

        public EventAttendees(int id, string name, string email, string other, int groupId) {
            Id = id;
            Name = name;
            Email = email;
            Other = other;
            GroupId = groupId;
        }

        public int Id { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public String Other { get; set; }
        public int GroupId { get; set; }
    }


}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageEvent.Dto
{
    public class Event
    {
        public Event()
        {
        }

        public Event(int id, string name, string description, List<CheckIn> checkIns, string status)
        {
            Id = id;
            Name = name;
            Description = description;
            this.checkIns = checkIns;
            Status = status;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<CheckIn> checkIns { get; set; }
        public string userId { get; set; }
        public string Status { get; set; }
    }
}

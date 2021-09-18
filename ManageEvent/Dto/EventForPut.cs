using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ManageEvent.Dto
{
    public class EventForPut
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Token { get; set; }
        public DateTime EventDateAt { get; set; }
    }
}


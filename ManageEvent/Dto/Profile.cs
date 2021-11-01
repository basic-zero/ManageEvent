using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageEvent.Dto {
    public class Profile {
        public string Name { get; set; }
        public string Email { get; set; }
        public int EventCount { get; set; }
        public int GroupCount { get; set; }
    }
}

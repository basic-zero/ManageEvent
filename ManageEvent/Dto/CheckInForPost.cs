using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageEvent.Dto
{
    public class CheckInForPost
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Other { get; set; }
        public string Token { get; set; }
    }
}

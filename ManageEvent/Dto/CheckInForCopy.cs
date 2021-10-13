using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageEvent.Dto
{
    public class CheckInForCopy
    {
        public int EventId { get; set; }
        public int GroupId { get; set; }
        public string Token { get; set; }
    }
}

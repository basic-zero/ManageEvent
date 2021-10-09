using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageEvent.Dto
{
    public class UserForLogin
    {
        public string Token { get; set; }
        public int Id { get; set; }
        public int Status { get; set; }
    }
}

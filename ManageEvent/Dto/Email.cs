using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageEvent.Dto
{
    public class Email
    {
        public Email()
        {
        }

        public Email(int eventId, string subject, string body, string token)
        {
            EventId = eventId;
            Subject = subject;
            Body = body;
            Token = token;
        }

        public int EventId { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Token { get; set; }
    }
}

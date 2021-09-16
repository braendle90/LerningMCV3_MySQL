using Microsoft.AspNetCore.Http;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LerningMCV3_MySQL.Models
{
    public class MailRequest
    {
        public string ToMail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Attachments { get; set; }

    }
}

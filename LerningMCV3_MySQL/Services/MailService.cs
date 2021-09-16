using LerningMCV3_MySQL.Models;
using LerningMCV3_MySQL.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LerningMCV3_MySQL.Services
{
    public class MailService : IMailService
    {

        private readonly MailSettings _mailSettings;

        public MailService(IOptions<MailSettings> options)
        {
            _mailSettings = options.Value;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToMail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();


            // Set the plain-text version of the message text
            builder.TextBody = @"Hey Alice,

lkdsfkldsjfkljdfkljkjdsklj  ljsdlfjlks
";

            // We may also want to attach a calendar event for Monica's party...
            builder.Attachments.Add(mailRequest.Attachments);

            // Now we just need to set the message body and we're done
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}

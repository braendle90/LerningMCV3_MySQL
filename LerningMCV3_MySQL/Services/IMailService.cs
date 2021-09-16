using LerningMCV3_MySQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LerningMCV3_MySQL.Services
{
    public interface IMailService
    {

        Task SendEmailAsync(MailRequest mailRequest);
    }
}

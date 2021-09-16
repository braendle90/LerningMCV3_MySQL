

using LerningMCV3_MySQL.Models;
using LerningMCV3_MySQL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LerningMCV3_MySQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {

        private readonly IMailService _mailService;

        public EmailController(IMailService mailService)
        {
            _mailService = mailService;
        }



 

    }


}

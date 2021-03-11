using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using cakeworld.Models;
using cakeworld.Services.MailService;
using cakeworld.Services.JWT_Services;

namespace cakeworld.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        public readonly OnlineDBContext _context;

        private readonly IConfiguration _config;
        private IMailService _mailService;
        private IJWTService _jwtService;

        public LoginsController(IConfiguration config, IJWTService jwtservice, OnlineDBContext context, IMailService mailService)
        {
            _config = config;
            _jwtService = jwtservice;
            _context = context;
            _mailService = mailService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Login>>> GetLogin()
        {
            return await _context.Logins.ToListAsync();
        }


        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            try
            {            // CustomerModelDB.Add(newcustomer);
                var CheckEmailBuyer = _context.Buyers.FirstOrDefault(m => m.Email.ToLower() == login.Email.ToLower()); //check email already exit or not
                var CheckPasswordBuyer = _context.Buyers.FirstOrDefault(m => m.Password == login.Password);

                var CheckEmailSeller = _context.Sellers.FirstOrDefault(m => m.Email.ToLower() == login.Email.ToLower()); //check email already exit or not
                var CheckPasswordSeller = _context.Sellers.FirstOrDefault(m => m.Password == login.Password);

                var CheckEmailAdmin = _context.Admins.FirstOrDefault(m => m.Email.ToLower() == login.Email.ToLower()); //check email already exit or not
                var CheckPasswordAdmin = _context.Admins.FirstOrDefault(m => m.Password == login.Password);




                if ((CheckEmailBuyer == null || CheckPasswordBuyer == null) && (CheckEmailSeller == null || CheckPasswordSeller == null) && (CheckEmailAdmin == null || CheckPasswordAdmin == null))
                {
                    return BadRequest(); //New page
                }



                else
                {
                    await _mailService.SendEmailAsync(login.Email, "New login", "<h1>Hey!, Did you login to your account</h1><p>New login to your account at " + DateTime.Now + "</p>");

                    // Return token
                    var tokenString = _jwtService.GenerateJWTtoken(login);
                    return Ok(new
                    {
                        token = tokenString
                    });


                }



            }





            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

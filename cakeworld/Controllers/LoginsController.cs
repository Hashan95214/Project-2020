using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using cakeworld.Models;

namespace cakeworld.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        public readonly OnlineDBContext _context;

        public readonly IConfiguration _config;

        public LoginsController(IConfiguration config ,OnlineDBContext context)
        {
            _context = context;
        }

        // GET: api/Logins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Login>>> GetLogins()
        {
            return await _context.Logins.ToListAsync();
        }

        // GET: api/Logins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Login>> GetLogin(int id)
        {
            var login = await _context.Logins.FindAsync(id);

            if (login == null)
            {
                return NotFound();
            }

            return login;
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(Login login)
        {
            try
            {            // CustomerModelDB.Add(newcustomer);
                var CheckEmailBuyer = _context.Buyers.FirstOrDefault(m => m.Email.ToLower() == login.Email.ToLower()); //check email already exit or not
                var CheckPasswordBuyer = _context.Buyers.FirstOrDefault(m => m.Password == login.Password);

              



                if (CheckEmailBuyer == null || CheckPasswordBuyer == null)
                {
                    return BadRequest(); //New page
                }



                else
                {

                    return Ok("completed");
                    
                }



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

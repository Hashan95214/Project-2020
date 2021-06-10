using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cakeworld.Models;
using cakeworld.Services.MailService;
using Microsoft.Extensions.Configuration;

namespace cakeworld.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellersController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly OnlineDBContext _context;
        public readonly OnlineDBContext _db;
        private IMailService _mailService;

        public SellersController(IConfiguration config, OnlineDBContext db, OnlineDBContext context, IMailService mailService)
        {
            _config = config;
            _context = context;
            _db = db;
            _mailService = mailService;
        }

        // GET: api/Sellers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Seller>>> GetSellers()
        {
            return await _context.Sellers.ToListAsync();
        }

        // GET: api/Sellers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Seller>> GetSeller(int id)
        {
            var seller = await _context.Sellers.FindAsync(id);

            if (seller == null)
            {
                return NotFound();
            }

            return seller;
        }

        // PUT: api/Sellers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeller(int id, Seller seller)
        {
            if (id != seller.Id)
            {
                return BadRequest();
            }

            _context.Entry(seller).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SellerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Sellers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Seller>> PostSeller(Seller seller)

        {
            // CustomerModelDB.Add(newcustomer);
            var SellerWithSameEmail = _db.Sellers.FirstOrDefault(m => m.Email.ToLower() == seller.Email.ToLower()); //check email already exit or not


            if (SellerWithSameEmail == null)
            {


                _context.Sellers.Add(seller);
                await _context.SaveChangesAsync();
                await _mailService.SendEmailAsync(seller.Email, "Registration Successful", "<h1>Hey!, You are succesfully registered as seller in cakeworld </h1><p>Create your account at " + DateTime.Now + "</p>");
                return CreatedAtAction("GetSeller", new { id = seller.Id }, seller);


            }
            else
            {
                return BadRequest();
            }

        }

        // DELETE: api/Sellers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Seller>> DeleteSeller(int id)
        {
            var seller = await _context.Sellers.FindAsync(id);
            if (seller == null)
            {
                return NotFound();
            }

            _context.Sellers.Remove(seller);
            await _context.SaveChangesAsync();

            return seller;
        }

        private bool SellerExists(int id)
        {
            return _context.Sellers.Any(e => e.Id == id);
        }
    }
}

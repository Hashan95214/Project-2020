﻿using System;
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
    public class BuyersController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly OnlineDBContext _context;
        public readonly OnlineDBContext _db;
        private IMailService _mailService;

        public BuyersController(IConfiguration config, OnlineDBContext db, OnlineDBContext context, IMailService mailService)
        {
            _config = config;
            _context = context;
            _db = db;
            _mailService = mailService;


        }

        // GET: api/Buyers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Buyer>>> GetBuyers()
        {
            return await _context.Buyers.ToListAsync();
        }

        // GET: api/Buyers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Buyer>> GetBuyer(int id)
        {
            var buyer = await _context.Buyers.FindAsync(id);

            if (buyer == null)
            {
                return NotFound();
            }

            return buyer;
        }

        // PUT: api/Buyers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBuyer(int id, Buyer buyer)
        {
            if (id != buyer.Id)
            {
                return BadRequest();
            }

            _context.Entry(buyer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuyerExists(id))
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

        // POST: api/Buyers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Buyer>> PostBuyer(Buyer buyer)


        {

            var BuyersWithSameEmail = _db.Buyers.FirstOrDefault(m => m.Email.ToLower() == buyer.Email.ToLower()); //check email already exit or not


            if (BuyersWithSameEmail == null)
            {


                _context.Buyers.Add(buyer);
                await _context.SaveChangesAsync();
                await _mailService.SendEmailAsync(buyer.Email, "Registration Successful  ", "<h1>Hey!, You are successfully registered as Buyer in cakeworld </h1><p>Create your account at " + DateTime.Now + "</p>");
                return CreatedAtAction("GetBuyer", new { id = buyer.Id }, buyer);


            }
            else
            {
                return BadRequest();
            }

        }

        // DELETE: api/Buyers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Buyer>> DeleteBuyer(int id)
        {
            var buyer = await _context.Buyers.FindAsync(id);
            if (buyer == null)
            {
                return NotFound();
            }

            _context.Buyers.Remove(buyer);
            await _context.SaveChangesAsync();

            return buyer;
        }

        private bool BuyerExists(int id)
        {
            return _context.Buyers.Any(e => e.Id == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using cakeworld.Models;
using cakeworld.Services;

namespace cakeworld.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly OnlineDBContext _context;
        private readonly IConfiguration _config;
        // IMailService _mailService;
        private MakePayment _makePayment;

        public PaymentsController(OnlineDBContext context, IConfiguration config, MakePayment makePayment)
        {
            _context = context;
            _config = config;
            /*_mailService = mailService;*/
            _makePayment = makePayment;
        }

        // GET: api/Payments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payments>>> GetPayment()
        {
            return await _context.Payments.ToListAsync();
        }

        // GET: api/Payments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Payments>> GetPayment(int id)
        {
            var payment = await _context.Payments.FindAsync(id);

            if (payment == null)
            {
                return NotFound();
            }

            return payment;
        }

        // PUT: api/Payments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayment(int id, Payments payment)
        {
            if (id != payment.Id)
            {
                return BadRequest();
            }

            _context.Entry(payment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentExists(id))
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

        // POST: api/Payments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Payments>> PostPayment(Payments payment)
        {
            await _makePayment.PayAsync(payment.CardNo, payment.ExpMonth, payment.ExpYear, payment.Cvv, payment.TotalPrice * 100);
            if (MakePayment.paymentStatus)
            {
                _context.Payments.Add(payment);
                await _context.SaveChangesAsync();
                /*await _mailService.SendEmailAsync
                (
                    payment.Email,
                    "Payment Confirmation for Bill No:" + payment.Id,
                    "<p><strong>Thank you for using Govimithuro!</strong></p>" +
                    " <p>This email is to confirm your recent transaction.</p>" +
                    "<p> Card Holder's Name:" + payment.CardName +
                    "<p>Card No :" + payment.CardNo +
                    "<p> <strong>Total: Rs." + payment.TotalPrice +
                    "<p>Date :" + DateTime.Now
                );*/
            }
            var newUser = CreatedAtAction("GetBillingInfo", new { id = payment.Id }, payment);

            if (newUser != null)
            {
                return Ok("Success");
            }
            else
            {
                return Ok("Error");
            }
        }

        // DELETE: api/Payments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Payments>> DeletePayment(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();

            return payment;
        }

        private bool PaymentExists(int id)
        {
            return _context.Payments.Any(e => e.Id == id);
        }
    }
}

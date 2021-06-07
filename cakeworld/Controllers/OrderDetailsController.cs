using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cakeworld.Models;

namespace cakeworld.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly OnlineDBContext _context;

        public OrderDetailsController(OnlineDBContext context)
        {
            _context = context;
        }

        // GET: api/OrderDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetails>>> GetOrderDetails()
        {
            return await _context.OrderDetails
                 .Select(x => new OrderDetails()
                 {
                     ID = x.ID,
                     CustomerId = x.CustomerId,
                     Quantity = x.Quantity,
                     OrderDate = x.OrderDate,
                     RequiredDate = x.RequiredDate,
                     OrderNumber = x.OrderNumber,
                     FirstName = x.FirstName,
                     LastName = x.LastName,
                     CompanyName = x.CompanyName,
                     AddressLine1 = x.AddressLine1,
                     AddressLine2 = x.AddressLine2,
                     Town = x.Town,
                     City = x.City,
                     PostalCode = x.PostalCode,
                     Phone = x.Phone,
                     Email = x.Email,
                     Method = x.Method
                 })
                .ToListAsync();
        }


        [HttpGet("{para}/{para2}")]
        public async Task<ActionResult<IEnumerable<OrderDetails>>> GetOr(string para, int para2)
        {
            return await _context.OrderDetails
                .Select(x => new OrderDetails()
                {
                    ID = x.ID,
                    CustomerId = x.CustomerId,
                    Quantity = x.Quantity,
                    OrderDate = x.OrderDate,
                    RequiredDate = x.RequiredDate,
                    OrderNumber = x.OrderNumber,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    CompanyName = x.CompanyName,
                    AddressLine1 = x.AddressLine1,
                    AddressLine2 = x.AddressLine2,
                    Town = x.Town,
                    City = x.City,
                    PostalCode = x.PostalCode,
                    Phone = x.Phone,
                    Email = x.Email,
                    Method = x.Method
                })
                .Where(i => i.CustomerId == para2)
                .ToListAsync();
        }



        // GET: api/OrderDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetails>> GetOrderDetails(int id)
        {
            var orderDetails = await _context.OrderDetails.FindAsync(id);

            if (orderDetails == null)
            {
                return NotFound();
            }

            return orderDetails;
        }

        // PUT: api/OrderDetails/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderDetails(int id, [FromForm] OrderDetails orderDetails)
        {
            if (id != orderDetails.ID)
            {
                return BadRequest();
            }

            _context.Entry(orderDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderDetailsExists(id))
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

        // POST: api/OrderDetails
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<OrderDetails>> PostOrderDetails([FromForm] OrderDetails orderDetails)
        {
            _context.OrderDetails.Add(orderDetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderDetails", new { id = orderDetails.ID }, orderDetails);
        }

        // DELETE: api/OrderDetails/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderDetails>> DeleteOrderDetails(int id)
        {
            var orderDetails = await _context.OrderDetails.FindAsync(id);
            if (orderDetails == null)
            {
                return NotFound();
            }

            _context.OrderDetails.Remove(orderDetails);
            await _context.SaveChangesAsync();

            return orderDetails;
        }

        private bool OrderDetailsExists(int id)
        {
            return _context.OrderDetails.Any(e => e.ID == id);
        }
    }
}

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
    public class OrderProductsController : ControllerBase
    {
        private readonly OnlineDBContext _context;

        public OrderProductsController(OnlineDBContext context)
        {
            _context = context;
        }

        // GET: api/OrderProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderProducts>>> GetOrderProducts()
        {
            return await _context.OrderProducts
                .Select(x => new OrderProducts()
                {
                    Id = x.Id,
                    OrderNo = x.OrderNo,
                    BuyerId = x.BuyerId,
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    UnitPrice = x.UnitPrice,
                    ProductQuantity = x.ProductQuantity,
                })
                .ToListAsync();
        }



        [HttpGet("{para}/{para2}")]
        public async Task<ActionResult<IEnumerable<OrderProducts>>> GetOrderProduct(string para, int para2)
        {
            return await _context.OrderProducts
                .Select(x => new OrderProducts()
                {
                    Id = x.Id,
                    OrderNo = x.OrderNo,
                    BuyerId = x.BuyerId,
                    ProductId = x.ProductId,
                    ProductName = x.ProductName,
                    UnitPrice = x.UnitPrice,
                    ProductQuantity = x.ProductQuantity,
                })
                .Where(i => i.BuyerId == para2)
                .ToListAsync();
        }



        // GET: api/OrderProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderProducts>> GetOrderProducts(int id)
        {
            var orderProducts = await _context.OrderProducts.FindAsync(id);

            if (orderProducts == null)
            {
                return NotFound();
            }

            return orderProducts;
        }

        // PUT: api/OrderProducts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderProducts(int id, [FromForm] OrderProducts orderProducts)
        {
            if (id != orderProducts.Id)
            {
                return BadRequest();
            }

            _context.Entry(orderProducts).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderProductsExists(id))
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

        // POST: api/OrderProducts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<OrderProducts>> PostOrderProducts([FromForm] OrderProducts orderProducts)
        {
            _context.OrderProducts.Add(orderProducts);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderProducts", new { id = orderProducts.Id }, orderProducts);
        }

        // DELETE: api/OrderProducts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<OrderProducts>> DeleteOrderProducts(int id)
        {
            var orderProducts = await _context.OrderProducts.FindAsync(id);
            if (orderProducts == null)
            {
                return NotFound();
            }

            _context.OrderProducts.Remove(orderProducts);
            await _context.SaveChangesAsync();

            return orderProducts;
        }

        private bool OrderProductsExists(int id)
        {
            return _context.OrderProducts.Any(e => e.Id == id);
        }
    }
}

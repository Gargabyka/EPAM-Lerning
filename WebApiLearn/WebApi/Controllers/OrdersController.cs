using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        OrdersContext db;
        public OrdersController(OrdersContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Orders>>> GetAsync()
        {

            return await db.Orders.ToListAsync(); 
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Orders>> Get(int id)
        {
            var orders = await db.Orders.SingleOrDefaultAsync(x => x.OrderID == id);

            if (orders == null)
            {
                return NotFound();
            }
            return new ObjectResult(orders);
        }
        
        [HttpPost]
        public async Task<ActionResult<Orders>> Post(Orders order)
        {
            if (order == null)
            {
                return BadRequest();
            }
 
            db.Orders.Add(order);
            await db.SaveChangesAsync();
            return Ok(order);
        }
        
        [HttpPut]
        public async Task<ActionResult<Orders>> Put(Orders order)
        {
            if (order == null)
            {
                return BadRequest();
            }
            if (!db.Orders.Any(x => x.OrderID == order.OrderID))
            {
                return NotFound();
            }
 
            db.Update(order);
            await db.SaveChangesAsync();
            return Ok(order);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<Orders>> Delete(int id)
        {
            var order = await db.Orders.SingleOrDefaultAsync(x => x.OrderID == id);

            if (order == null)
            {
                return NotFound();
            }
            db.Orders.Remove(order);
            await db.SaveChangesAsync();
            return Ok(order);
        }
    }
}

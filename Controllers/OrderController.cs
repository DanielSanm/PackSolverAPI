using CromulentBisgetti.ContainerPacking;
using CromulentBisgetti.ContainerPacking.Algorithms;
using CromulentBisgetti.ContainerPacking.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using PackSolverAPI.DbContexts;
using PackSolverAPI.DTO;
using PackSolverAPI.Models;
using PackSolverAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace PackSolverAPI.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("pack")]
        public async Task<IActionResult> pack([FromBody] OrderPackingRequest request)
        {
            //var order = await _context.orders.FindAsync(request.OrderId);
            //if (order == null)
            //    return NotFound();

            var items = request.Products
                .Select((p, index) => new Item(index, p.Height, p.Width, p.Length, 1))
                .ToList();
            var boxes = await _context.boxes.ToListAsync();
            var result = new PackService().Pack(items, boxes);

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAll()
        {
            return await _context.orders.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> Get(int id)
        {
            var order = await _context.orders.FindAsync(id);
            if (order == null)
                return NotFound();

            return order;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> Post(Order order)
        {
            _context.orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = order.OrderId }, order);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Order order)
        {
            if (id != order.OrderId)
                return BadRequest();

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.orders.Any(o => o.OrderId == id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _context.orders.FindAsync(id);
            if (order == null)
                return NotFound();

            _context.orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PackSolverAPI.DbContexts;
using PackSolverAPI.Models;

namespace PackSolverAPI.Controllers
{
    [ApiController]
    [Route("api/boxes")]
    public class BoxController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BoxController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Box>>> GetAll()
        {
            return await _context.boxes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Box>> Get(int id)
        {
            var box = await _context.boxes.FindAsync(id);
            if (box == null)
                return NotFound();

            return box;
        }

        [HttpPost]
        public async Task<ActionResult<Box>> Post(Box box)
        {
            _context.boxes.Add(box);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = box.BoxId }, box);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, Box box)
        {
            if (id != box.BoxId)
                return BadRequest();

            _context.Entry(box).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.boxes.Any(b => b.BoxId == id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var box = await _context.boxes.FindAsync(id);
            if (box == null)
                return NotFound();

            _context.boxes.Remove(box);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

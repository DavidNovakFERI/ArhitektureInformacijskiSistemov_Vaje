using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EkipaController : ControllerBase
    {
        private readonly Formula1Context _context;

        public EkipaController(Formula1Context context)
        {
            _context = context;
        }

        

        

        // GET: api/Ekipa
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ekipa>>> GetEkipe()
        {
            return await _context.Ekipe.ToListAsync();
        }

        // GET: api/Ekipa/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ekipa>> GetEkipa(int id)
        {
            var ekipa = await _context.Ekipe.FindAsync(id);

            if (ekipa == null)
            {
                return NotFound();
            }

            return ekipa;
        }

        // POST: api/Ekipa
        [HttpPost]
        public async Task<ActionResult<Ekipa>> PostEkipa(Ekipa ekipa)
        {
            _context.Ekipe.Add(ekipa);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEkipa), new { id = ekipa.Id }, ekipa);
        }

        // PUT: api/Ekipa/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEkipa(int id, Ekipa ekipa)
        {
            if (id != ekipa.Id)
            {
                return BadRequest();
            }

            _context.Entry(ekipa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EkipaExists(id))
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

        // DELETE: api/Ekipa/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEkipa(int id)
        {
            var ekipa = await _context.Ekipe.FindAsync(id);
            if (ekipa == null)
            {
                return NotFound();
            }

            _context.Ekipe.Remove(ekipa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EkipaExists(int id)
        {
            return _context.Ekipe.Any(e => e.Id == id);
        }
    }
}

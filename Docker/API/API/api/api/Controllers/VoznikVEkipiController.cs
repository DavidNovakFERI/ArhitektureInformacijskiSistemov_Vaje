using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoznikVEkipiController : ControllerBase
    {
        private readonly Formula1Context _context;

        public VoznikVEkipiController(Formula1Context context)
        {
            _context = context;
        }

        // GET: api/VoznikVEkipi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VoznikVEkipi>>> GetVoznikiVEkipi()
        {
            return await _context.VoznikiVEkipi
                .Include(v => v.Voznik)
                .Include(e => e.Ekipa)
                .ToListAsync();
        }

        // GET: api/VoznikVEkipi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VoznikVEkipi>> GetVoznikVEkipi(int id)
        {
            var voznikVEkipi = await _context.VoznikiVEkipi
                .Include(v => v.Voznik)
                .Include(e => e.Ekipa)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (voznikVEkipi == null)
            {
                return NotFound();
            }

            return voznikVEkipi;
        }

        [HttpPost]
        public async Task<ActionResult<VoznikVEkipi>> PostVoznikVEkipi(VoznikVEkipiDto dto)
        {
            // Najprej preverimo, če voznik in ekipa obstajata v bazi
            var voznik = await _context.Vozniki.FindAsync(dto.VoznikId);
            var ekipa = await _context.Ekipe.FindAsync(dto.EkipaId);

            if (voznik == null || ekipa == null)
            {
                return BadRequest("Voznik ali ekipa ne obstajata.");
            }

            // Ustvarimo novo entiteto VoznikVEkipi
            var voznikVEkipi = new VoznikVEkipi
            {
                VoznikId = dto.VoznikId,
                EkipaId = dto.EkipaId,
                Voznik = voznik,
                Ekipa = ekipa,
                LetoOd = dto.LetoOd,
                LetoDo = dto.LetoDo,
                SteviloZmag = dto.SteviloZmag
            };

            _context.VoznikiVEkipi.Add(voznikVEkipi);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVoznikVEkipi), new { id = voznikVEkipi.Id }, voznikVEkipi);
        }




        // PUT: api/VoznikVEkipi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVoznikVEkipi(int id, VoznikVEkipi voznikVEkipi)
        {
            if (id != voznikVEkipi.Id)
            {
                return BadRequest();
            }

            _context.Entry(voznikVEkipi).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoznikVEkipiExists(id))
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

        // DELETE: api/VoznikVEkipi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVoznikVEkipi(int id)
        {
            var voznikVEkipi = await _context.VoznikiVEkipi.FindAsync(id);
            if (voznikVEkipi == null)
            {
                return NotFound();
            }

            _context.VoznikiVEkipi.Remove(voznikVEkipi);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Pomožna metoda za preverjanje obstoja voznikVEkipi
        private bool VoznikVEkipiExists(int id)
        {
            return _context.VoznikiVEkipi.Any(e => e.Id == id);
        }

        // POST: api/VoznikVEkipi/AddVoznikToEkipa
        [HttpPost("AddVoznikToEkipa")]
        public async Task<IActionResult> AddVoznikToEkipa(int voznikId, int ekipaId, int letoOd, int letoDo, int steviloZmag)
        {
            var voznik = await _context.Vozniki.FindAsync(voznikId);
            var ekipa = await _context.Ekipe.FindAsync(ekipaId);

            if (voznik == null || ekipa == null)
            {
                return BadRequest("Voznik ali ekipa ne obstajata.");
            }

            var voznikVEkipi = new VoznikVEkipi
            {
                VoznikId = voznikId,
                EkipaId = ekipaId,
                LetoOd = letoOd,
                LetoDo = letoDo,
                SteviloZmag = steviloZmag
            };

            _context.VoznikiVEkipi.Add(voznikVEkipi);
            await _context.SaveChangesAsync();

            return Ok(voznikVEkipi);
        }

        // DELETE: api/VoznikVEkipi/RemoveVoznikFromEkipa
        [HttpDelete("RemoveVoznikFromEkipa")]
        public async Task<IActionResult> RemoveVoznikFromEkipa(int voznikId, int ekipaId)
        {
            var voznikVEkipi = await _context.VoznikiVEkipi
                .FirstOrDefaultAsync(v => v.VoznikId == voznikId && v.EkipaId == ekipaId);

            if (voznikVEkipi == null)
            {
                return NotFound("Povezava med voznikom in ekipo ni bila najdena.");
            }

            _context.VoznikiVEkipi.Remove(voznikVEkipi);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

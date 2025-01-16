using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoznikController : ControllerBase
    {
        private readonly Formula1Context _context;

        public VoznikController(Formula1Context context)
        {
            _context = context;
        }

        

        // GET: api/Voznik
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Voznik>>> GetVozniki()
        {
            return await _context.Vozniki.ToListAsync();
        }

        // GET: api/Voznik/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Voznik>> GetVoznik(int id)
        {
            var voznik = await _context.Vozniki.FindAsync(id);

            if (voznik == null)
            {
                return NotFound();
            }

            return voznik;
        }

        // POST: api/Voznik
        [HttpPost]
        public async Task<ActionResult<Voznik>> PostVoznik(Voznik voznik)
        {
            _context.Vozniki.Add(voznik);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVoznik), new { id = voznik.Id }, voznik);
        }


        // PUT: api/Voznik/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVoznik(int id, Voznik voznik)
        {
            if (id != voznik.Id)
            {
                return BadRequest();
            }

            _context.Entry(voznik).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoznikExists(id))
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

        // DELETE: api/Voznik/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVoznik(int id)
        {
            var voznik = await _context.Vozniki.FindAsync(id);
            if (voznik == null)
            {
                return NotFound();
            }

            _context.Vozniki.Remove(voznik);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("AddVoznikWithTeamAssignment")]
        public async Task<IActionResult> AddVoznikWithTeamAssignment(VoznikWithTeamDto dto)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // 1. Dodaj novega voznika
                    var voznik = new Voznik
                    {
                        Ime = dto.Ime,
                        Priimek = dto.Priimek,
                        LetoRojstva = dto.LetoRojstva
                    };

                    _context.Vozniki.Add(voznik);
                    await _context.SaveChangesAsync();

                    // 2. Dodaj povezavo voznika z ekipo (VoznikVEkipi)
                    var voznikVEkipi = new VoznikVEkipi
                    {
                        VoznikId = voznik.Id,
                        EkipaId = dto.EkipaId,
                        LetoOd = dto.LetoOd,
                        LetoDo = dto.LetoDo,
                        SteviloZmag = dto.SteviloZmag
                    };

                    _context.VoznikiVEkipi.Add(voznikVEkipi);
                    await _context.SaveChangesAsync();

                    // Potrdi transakcijo
                    await transaction.CommitAsync();

                    return Ok(new { Voznik = voznik, VoznikVEkipi = voznikVEkipi });
                }
                catch
                {
                    // V primeru napake, povrnemo transakcijo
                    await transaction.RollbackAsync();
                    return StatusCode(500, "Prišlo je do napake med izvajanjem transakcije.");
                }
            }
        }

        private bool VoznikExists(int id)
        {
            return _context.Vozniki.Any(e => e.Id == id);
        }
    }
}

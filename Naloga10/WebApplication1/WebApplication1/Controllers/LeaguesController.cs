using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [Route("odata/Leagues")]
    public class LeaguesController : ODataController
    {
        private readonly FootballContext _context;

        public LeaguesController(FootballContext context)
        {
            _context = context;
        }

        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_context.Leagues);
        }

        [EnableQuery]
        public async Task<IActionResult> Get([FromODataUri] int key)
        {
            var league = await _context.Leagues.FindAsync(key);
            if (league == null)
            {
                return NotFound();
            }
            return Ok(league);
        }

        public async Task<IActionResult> Post([FromBody] League league)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Leagues.Add(league);
            await _context.SaveChangesAsync();

            return Created(league);
        }

        

        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            var league = await _context.Leagues.FindAsync(key);
            if (league == null)
            {
                return NotFound();
            }

            _context.Leagues.Remove(league);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

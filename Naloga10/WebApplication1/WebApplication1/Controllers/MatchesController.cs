using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [Route("odata/Matches")]
    public class MatchesController : ODataController
    {
        private readonly FootballContext _context;

        public MatchesController(FootballContext context)
        {
            _context = context;
        }

        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_context.Matches);
        }

        [EnableQuery]
        public async Task<IActionResult> Get([FromODataUri] int key)
        {
            var match = await _context.Matches.FindAsync(key);
            if (match == null)
            {
                return NotFound();
            }
            return Ok(match);
        }

        public async Task<IActionResult> Post([FromBody] Match match)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Matches.Add(match);
            await _context.SaveChangesAsync();

            return Created(match);
        }

        

        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            var match = await _context.Matches.FindAsync(key);
            if (match == null)
            {
                return NotFound();
            }

            _context.Matches.Remove(match);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

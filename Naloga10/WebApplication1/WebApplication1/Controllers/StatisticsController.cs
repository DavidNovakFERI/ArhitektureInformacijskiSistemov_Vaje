using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [Route("odata/Statistics")]
    public class StatisticsController : ODataController
    {
        private readonly FootballContext _context;

        public StatisticsController(FootballContext context)
        {
            _context = context;
        }

        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_context.Statistics);
        }

        [EnableQuery]
        public async Task<IActionResult> Get([FromODataUri] int key)
        {
            var statistic = await _context.Statistics.FindAsync(key);
            if (statistic == null)
            {
                return NotFound();
            }
            return Ok(statistic);
        }

        public async Task<IActionResult> Post([FromBody] Statistic statistic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Statistics.Add(statistic);
            await _context.SaveChangesAsync();

            return Created(statistic);
        }

        

        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            var statistic = await _context.Statistics.FindAsync(key);
            if (statistic == null)
            {
                return NotFound();
            }

            _context.Statistics.Remove(statistic);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

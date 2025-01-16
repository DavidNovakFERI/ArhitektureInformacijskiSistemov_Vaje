using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [Route("odata/Teams")]  
    public class TeamsController : ODataController
    {
        private readonly FootballContext _context;

        public TeamsController(FootballContext context)
        {
            _context = context;
        }

        // ETag Method
        private string GenerateETag(Team team)
        {
            var etagSource = $"{team.TeamId}-{team.Name}-{team.Coach}";
            var etagBytes = Encoding.UTF8.GetBytes(etagSource);
            var hash = SHA256.HashData(etagBytes);
            return Convert.ToBase64String(hash);
        }

        // GET: odata/Teams
        [EnableQuery]
        public IActionResult Get()
        {
            
            return Ok(_context.Teams);
        }

        // GET: odata/Teams(1)
        [EnableQuery]
        public async Task<IActionResult> Get([FromODataUri] int key)
        {
            var team = await _context.Teams.FindAsync(key);
            if (team == null)
            {
                return NotFound();
            }

            //ETag
            var etag = GenerateETag(team);

            //ETag header
            var response = Ok(team);
            Response.Headers["ETag"] = etag;

            return response;
        }

        // POST: odata/Teams
        public async Task<IActionResult> Post([FromBody] Team team)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Teams.Add(team);
            await _context.SaveChangesAsync();

            
            return Created(team);
        }

        // PUT: odata/Teams(1)
        public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] Team update, [FromHeader(Name = "If-Match")] string ifMatch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != update.TeamId)
            {
                return BadRequest("Mismatched Team ID");
            }

            var team = await _context.Teams.FindAsync(key);
            if (team == null)
            {
                return NotFound();
            }

            //ETag
            var currentETag = GenerateETag(team);

            if (ifMatch != currentETag)
            {
                return StatusCode(StatusCodes.Status412PreconditionFailed, "ETag does not match.");
            }

            team.Name = update.Name;
            team.Coach = update.Coach;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Teams.Any(e => e.TeamId == key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //ETag header
            var updatedResponse = Updated(team);
            Response.Headers["ETag"] = GenerateETag(team);

            return updatedResponse;
        }

        // DELETE: odata/Teams(1)
        public async Task<IActionResult> Delete([FromODataUri] int key, [FromHeader(Name = "If-Match")] string ifMatch)
        {
            var team = await _context.Teams.FindAsync(key);
            if (team == null)
            {
                return NotFound();
            }

            // ETag logika
            if (!string.IsNullOrEmpty(ifMatch))
            {
                var currentETag = GenerateETag(team);
                if (ifMatch != currentETag)
                {
                    return StatusCode(StatusCodes.Status412PreconditionFailed, "ETag does not match.");
                }
            }

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

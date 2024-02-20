using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BasketballAPI.Context;
using BasketballDataModel;

namespace BasketballAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly BasketballContext _context;
        private readonly ILogger<TeamsController> logger;

        public TeamsController(BasketballContext context, ILogger<TeamsController> logger)
        {
            _context = context;
            this.logger = logger;
        }

        // GET: api/Teams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamModel>>> GetTeams(CancellationToken cancellation = default(CancellationToken))
        {
            return await _context.Teams.ToListAsync();
        }

        // GET: api/Teams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TeamModel>> GetTeamModel(int id, CancellationToken cancellation = default(CancellationToken))
        {
            var teamModel = await _context.Teams.FindAsync(id);

            if (teamModel == null)
            {
                return NotFound();
            }

            return teamModel;
        }

        // PUT: api/Teams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeamModel(int id, TeamModel teamModel, CancellationToken cancellation = default(CancellationToken))
        {
            if (id != teamModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(teamModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamModelExists(id))
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

        // POST: api/Teams
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TeamModel>> PostTeamModel(TeamModel teamModel, CancellationToken cancellation = default(CancellationToken))
        {
            _context.Teams.Add(teamModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTeamModel", new { id = teamModel.Id }, teamModel);
        }

        // DELETE: api/Teams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeamModel(int id, CancellationToken cancellation = default(CancellationToken))
        {
            var teamModel = await _context.Teams.FindAsync(id);
            if (teamModel == null)
            {
                return NotFound();
            }

            _context.Teams.Remove(teamModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TeamModelExists(int id, CancellationToken cancellation = default(CancellationToken))
        {
            return _context.Teams.Any(e => e.Id == id);
        }
    }
}

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
    public class PlayersController : ControllerBase
    {
        private readonly BasketballContext _context;
        private readonly ILogger<PlayersController> logger;

        public PlayersController(BasketballContext context, ILogger<PlayersController> logger)
        {
            _context = context;
            this.logger = logger;
        }

        // GET: api/PlayerModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerModel>>> GetPlayers(CancellationToken cancellation = default(CancellationToken))
        {
            return await _context.Players.ToListAsync();
        }

        // GET: api/PlayerModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerModel>> GetPlayerModel(int id, CancellationToken cancellation = default(CancellationToken))
        {
            var playerModel = await _context.Players.FindAsync(id);

            if (playerModel == null)
            {
                return NotFound();
            }

            return playerModel;
        }

        // PUT: api/PlayerModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayerModel(int id, PlayerModel playerModel, CancellationToken cancellation = default(CancellationToken))
        {
            if (id != playerModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(playerModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerModelExists(id))
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

        // POST: api/PlayerModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PlayerModel>> PostPlayerModel(PlayerModel playerModel, CancellationToken cancellation = default(CancellationToken))
        {
            _context.Players.Add(playerModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlayerModel", new { id = playerModel.Id }, playerModel);
        }

        // DELETE: api/PlayerModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayerModel(int id, CancellationToken cancellation = default(CancellationToken))
        {
            var playerModel = await _context.Players.FindAsync(id);
            if (playerModel == null)
            {
                return NotFound();
            }

            _context.Players.Remove(playerModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlayerModelExists(int id)
        {
            return _context.Players.Any(e => e.Id == id);
        }
    }
}

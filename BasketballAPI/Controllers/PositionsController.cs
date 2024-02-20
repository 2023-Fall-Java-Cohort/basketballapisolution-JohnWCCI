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
    public class PositionsController : ControllerBase
    {
        private readonly BasketballContext _context;
        private readonly ILogger<PositionsController> logger;

        public PositionsController(BasketballContext context, ILogger<PositionsController> logger)
        {
            _context = context;
            this.logger = logger;
        }

        // GET: api/Position
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PositionModel>>> GetPositions(CancellationToken cancellation = default(CancellationToken))
        {
            return await _context.Positions.ToListAsync();
        }

        // GET: api/Position/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PositionModel>> GetPositionModel(int id, CancellationToken cancellation = default(CancellationToken))
        {
            var positionModel = await _context.Positions.FindAsync(id);

            if (positionModel == null)
            {
                return NotFound();
            }

            return positionModel;
        }

        // PUT: api/Position/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPositionModel(int id, PositionModel positionModel, CancellationToken cancellation = default(CancellationToken))
        {
            if (id != positionModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(positionModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PositionModelExists(id))
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

        // POST: api/Position
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PositionModel>> PostPositionModel(PositionModel positionModel,CancellationToken cancellation = default(CancellationToken))
        {
            _context.Positions.Add(positionModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPositionModel", new { id = positionModel.Id }, positionModel);
        }

        // DELETE: api/Position/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePositionModel(int id, CancellationToken cancellation = default(CancellationToken))
        {
            var positionModel = await _context.Positions.FindAsync(id);
            if (positionModel == null)
            {
                return NotFound();
            }

            _context.Positions.Remove(positionModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PositionModelExists(int id)
        {
            return _context.Positions.Any(e => e.Id == id);
        }
    }
}

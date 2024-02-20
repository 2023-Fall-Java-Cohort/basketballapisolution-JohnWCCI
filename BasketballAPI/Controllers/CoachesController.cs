using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BasketballAPI.Context;
using BasketballDataModel;

namespace BasketballAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CoachesController : ControllerBase
    {
        private readonly BasketballContext _context;
        private readonly ILogger<CoachesController> logger;

        public CoachesController(BasketballContext context, ILogger<CoachesController> logger)
        {
            _context = context;
            this.logger = logger;
        }

        // GET: api/Coaches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CoachModel>>> GetCoaches(CancellationToken cancellation = default(CancellationToken))
        {
            return await _context.Coaches.ToListAsync();
        }

        // GET: api/Coaches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CoachModel>> GetCoachModel(int id, CancellationToken cancellation = default(CancellationToken))
        {
            var coachModel = await _context.Coaches.FindAsync(id);

            if (coachModel == null)
            {
                return NotFound();
            }

            return coachModel;
        }

        // PUT: api/Coaches/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoachModel(int id, CoachModel coachModel, CancellationToken cancellation = default(CancellationToken))
        {
            if (id != coachModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(coachModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoachModelExists(id))
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

        // POST: api/Coaches
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CoachModel>> PostCoachModel(CoachModel coachModel, CancellationToken cancellation = default(CancellationToken))
        {
            _context.Coaches.Add(coachModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoachModel", new { id = coachModel.Id }, coachModel);
        }

        // DELETE: api/Coaches/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoachModel(int id, CancellationToken cancellation = default(CancellationToken))
        {
            var coachModel = await _context.Coaches.FindAsync(id);
            if (coachModel == null)
            {
                return NotFound();
            }

            _context.Coaches.Remove(coachModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CoachModelExists(int id)
        {
            return _context.Coaches.Any(e => e.Id == id);
        }
    }
}

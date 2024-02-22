using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BasketballAPI.Context;
using BasketballDataModel;

namespace BasketballApp.Controllers
{
    public class PlayerModelsController : Controller
    {
        private readonly BasketballContext _context;

        public PlayerModelsController(BasketballContext context)
        {
            _context = context;
        }

        // GET: PlayerModels
        public async Task<IActionResult> Index()
        {
            var basketballContext = _context.Players.Include(p => p.Position).Include(p => p.Team);
            return View(await basketballContext.ToListAsync());
        }

        // GET: PlayerModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playerModel = await _context.Players
                .Include(p => p.Position)
                .Include(p => p.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playerModel == null)
            {
                return NotFound();
            }

            return View(playerModel);
        }

        // GET: PlayerModels/Create
        public IActionResult Create()
        {
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "Name");
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name");
            return View();
        }

        // POST: PlayerModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NationalRank,Birthdate,IsActive,Salary,TeamId,PositionId,Id,Name,IsDeleted,UpdateOn,CreateDate")] PlayerModel playerModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(playerModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "Name", playerModel.PositionId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", playerModel.TeamId);
            return View(playerModel);
        }

        // GET: PlayerModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playerModel = await _context.Players.FindAsync(id);
            if (playerModel == null)
            {
                return NotFound();
            }
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "Name", playerModel.PositionId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", playerModel.TeamId);
            return View(playerModel);
        }

        // POST: PlayerModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NationalRank,Birthdate,IsActive,Salary,TeamId,PositionId,Id,Name,IsDeleted,UpdateOn,CreateDate")] PlayerModel playerModel)
        {
            if (id != playerModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(playerModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerModelExists(playerModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PositionId"] = new SelectList(_context.Positions, "Id", "Name", playerModel.PositionId);
            ViewData["TeamId"] = new SelectList(_context.Teams, "Id", "Name", playerModel.TeamId);
            return View(playerModel);
        }

        // GET: PlayerModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playerModel = await _context.Players
                .Include(p => p.Position)
                .Include(p => p.Team)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (playerModel == null)
            {
                return NotFound();
            }

            return View(playerModel);
        }

        // POST: PlayerModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var playerModel = await _context.Players.FindAsync(id);
            if (playerModel != null)
            {
                _context.Players.Remove(playerModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayerModelExists(int id)
        {
            return _context.Players.Any(e => e.Id == id);
        }
    }
}

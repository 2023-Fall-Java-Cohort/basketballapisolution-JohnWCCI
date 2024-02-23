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
    public class PositionController : Controller
    {
        private readonly BasketballContext _context;

        public PositionController(BasketballContext context)
        {
            _context = context;
        }

        // GET: PositionModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Positions.ToListAsync());
        }

        // GET: PositionModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var positionModel = await _context.Positions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (positionModel == null)
            {
                return NotFound();
            }

            return View(positionModel);
        }

        // GET: PositionModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PositionModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,IsDeleted,UpdateOn,CreateDate")] PositionModel positionModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(positionModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(positionModel);
        }

        // GET: PositionModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var positionModel = await _context.Positions.FindAsync(id);
            if (positionModel == null)
            {
                return NotFound();
            }
            return View(positionModel);
        }

        // POST: PositionModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,IsDeleted,UpdateOn,CreateDate")] PositionModel positionModel)
        {
            if (id != positionModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(positionModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PositionModelExists(positionModel.Id))
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
            return View(positionModel);
        }

        // GET: PositionModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var positionModel = await _context.Positions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (positionModel == null)
            {
                return NotFound();
            }

            return View(positionModel);
        }

        // POST: PositionModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var positionModel = await _context.Positions.FindAsync(id);
            if (positionModel != null)
            {
                _context.Positions.Remove(positionModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PositionModelExists(int id)
        {
            return _context.Positions.Any(e => e.Id == id);
        }
    }
}

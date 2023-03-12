using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BussinessObject.DataAccess;
using Microsoft.AspNetCore.Authorization;

namespace AMSWebClient.Controllers
{
    public class ClassesController : Controller
    {
        private readonly AMSContext _context;

        public ClassesController(AMSContext context)
        {
            _context = context;
        }

        // GET: Classes
        public async Task<IActionResult> Index()
        {
            //var aMSContext = _context.Classes.Include(c => c.Teacher);
            //return View(await aMSContext.ToListAsync());
            return View();
        }

        // GET: Classes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Classes == null)
            {
                return NotFound();
            }

            var cclass = await _context.Classes
                .Include(c => c.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cclass == null)
            {
                return NotFound();
            }

            return View(cclass);
        }

        // GET: Classes/Create
        public IActionResult Create()
        {
            ViewData["TeacherId"] = new SelectList(_context.Users, "Id", "FullName");
            return View();
        }

        // POST: Classes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClassName,ClassDescription,ClassCode,ClassStatus,TeacherId,CreateAt")] Class cclass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cclass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TeacherId"] = new SelectList(_context.Users, "Id", "FullName", cclass.TeacherId);
            return View(cclass);
        }

        // GET: Classes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Classes == null)
            {
                return NotFound();
            }

            var @class = await _context.Classes.FindAsync(id);
            if (@class == null)
            {
                return NotFound();
            }
            ViewData["TeacherId"] = new SelectList(_context.Users, "Id", "FullName", @class.TeacherId);
            return View(@class);
        }

        // POST: Classes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClassName,ClassDescription,ClassCode,ClassStatus,TeacherId,CreateAt")] Class @class)
        {
            if (id != @class.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@class);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassExists(@class.Id))
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
            ViewData["TeacherId"] = new SelectList(_context.Users, "Id", "FullName", @class.TeacherId);
            return View(@class);
        }

        // GET: Classes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Classes == null)
            {
                return NotFound();
            }

            var cclass = await _context.Classes
                .Include(c => c.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cclass == null)
            {
                return NotFound();
            }

            return View(cclass);
        }

        // POST: Classes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Classes == null)
            {
                return Problem("Entity set 'AMSContext.Classes'  is null.");
            }
            var cclass = await _context.Classes.FindAsync(id);
            if (cclass != null)
            {
                _context.Classes.Remove(cclass);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassExists(int id)
        {
          return _context.Classes.Any(e => e.Id == id);
        }
    }
}

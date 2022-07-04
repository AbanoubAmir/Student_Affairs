using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Student_Affairs.Data;
using Student_Affairs.Models;
using Student_Affairs.Models.Helpers;

namespace Student_Affairs.Controllers
{
    public class ClassesController : Controller
    {
        private readonly StudentAffairsContext _context;

        public ClassesController(StudentAffairsContext context)
        {
            _context = context;
        }

        // GET: Classes
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var classes = from s in _context.Classes
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                classes = classes.Where(s => s.Name.Contains(searchString));
            }
            if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "Name";
            }
            bool descending = false;
            if (sortOrder.EndsWith("_desc"))
            {
                sortOrder = sortOrder[..^5];
                descending = true;
            }

            if (descending)
            {
                classes = classes.OrderByDescending(e => EF.Property<object>(e, sortOrder));
            }
            else
            {
                classes = classes.OrderBy(e => EF.Property<object>(e, sortOrder));
            }
            //TODO: User selects pagesize
            int pageSize = 10;
            return View(await PaginatedList<Class>.CreateAsync(classes.AsNoTracking(),
                pageNumber ?? 1, pageSize));
        }

        // GET: Classes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @class = await _context.Classes
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.ID == id);
            if (@class == null)
            {
                return NotFound();
            }

            return View(@class);
        }

        // GET: Classes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Classes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] Class @class)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(@class);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError(ex.Message, "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(@class);
        }

        // GET: Classes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @class = await _context.Classes.FindAsync(id);
            if (@class == null)
            {
                return NotFound();
            }
            return View(@class);
        }

        // POST: Classes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name")] Class @class)
        {
            if (id != @class.ID)
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
                    if (!ClassExists(@class.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id });
            }
            return View(@class);
        }

        // GET: Classes/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false, string errorMessage = "")
        {
            if (id == null)
            {
                return NotFound();
            }

            var @class = await _context.Classes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (@class == null)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.\n" +
                    errorMessage;
            }
            return View(@class);
        }

        // POST: Classes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if(_context.Students.Any(s => s.ClassID == id))
            {
                return RedirectToAction(nameof(Delete), new { id, saveChangesError = true, errorMessage = "Can not delete a class that has students enrolled in it" });
            }
            var @class = await _context.Classes.FindAsync(id);
            if(@class == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _context.Classes.Remove(@class);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                return RedirectToAction(nameof(Delete), new { id, saveChangesError = true, errorMessage = ex.Message });
            }

        }

        private bool ClassExists(int id)
        {
            return _context.Classes.Any(e => e.ID == id);
        }
    }
}

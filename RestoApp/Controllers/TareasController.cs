using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestoApp.Data;
using RestoApp.Models;
using RestoApp.ViewModel;

namespace RestoApp.Controllers
{
    public class TareasController : Controller
    {
        private readonly RestoAppDB _context;

        public TareasController(RestoAppDB context)
        {
            _context = context;
        }

        // GET: Tareas
        public IActionResult Index()
        {

            var tareas = from e in _context.Tasks
                            join r in _context.Areas
                            on e.Area_ID equals r.Area_ID
                            select new TareaOutput
                            {
                                Task_ID = e.Task_ID,
                                Area_ID = e.Area_ID,
                                Task_Description = e.Task_Description,
                                Employee_ID = e.Employee_ID,
                                Area_Name = r.Area_Name
                            };

            EmployeeViewModel viewModel_B = new EmployeeViewModel();
            viewModel_B.Tareas = tareas.ToList();

            return View(viewModel_B);
        }

        // GET: Tareas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarea = await _context.Tasks
                .FirstOrDefaultAsync(m => m.Task_ID == id);
            if (tarea == null)
            {
                return NotFound();
            }

            return View(tarea);
        }

        // GET: Tareas/Create
        public IActionResult Create()
        {

            ViewBag.Areas = new SelectList(_context.Areas.ToList(), "Area_ID", "Area_Name");
            ViewBag.Empleados = new SelectList(_context.Employees.ToList(), "Employee_ID", "First_Name");
            return View();
        }

        // POST: Tareas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Task_ID,Area_ID,Task_Description,Employee_ID")] Tarea tarea)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tarea);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tarea);
        }

        // GET: Tareas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarea = await _context.Tasks.FindAsync(id);
            if (tarea == null)
            {
                return NotFound();
            }
            return View(tarea);
        }

        // POST: Tareas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Task_ID,Area_ID,Task_Description,Employee_ID")] Tarea tarea)
        {
            if (id != tarea.Task_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tarea);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TareaExists(tarea.Task_ID))
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
            return View(tarea);
        }

        // GET: Tareas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarea = await _context.Tasks
                .FirstOrDefaultAsync(m => m.Task_ID == id);
            if (tarea == null)
            {
                return NotFound();
            }

            return View(tarea);
        }

        // POST: Tareas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tarea = await _context.Tasks.FindAsync(id);
            _context.Tasks.Remove(tarea);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TareaExists(int id)
        {
            return _context.Tasks.Any(e => e.Task_ID == id);
        }
    }
}

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
   

    public class EmployeesController : Controller 
        
        
    {

        private readonly RestoAppDB _context; //Este context es inyectado en el servicio del controller 


        public EmployeesController(RestoAppDB context)
        {
            _context = context;
        }

        
        // GET: Employees
        public IActionResult Index(string searchString)
        {


             var employees = from e in _context.Employees
                            join r in _context.Areas
                            on e.Area_ID equals r.Area_ID
                            select new EmployeeOutput {
                                Employee_ID = e.Employee_ID,
                                First_Name = e.First_Name,
                                Last_Name = e.Last_Name,
                                Dni = e.Dni,
                                Area_ID = e.Area_ID,
                                Area_Name = r.Area_Name
                            };

            EmployeeViewModel viewModel = new EmployeeViewModel();
            viewModel.Employees = employees.ToList();

            

            if (!String.IsNullOrEmpty(searchString))
            {
                employees = employees.Where(s => s.First_Name.Contains(searchString));
            }

            return View(viewModel);
        }
        

  

        [HttpPost]
        public string Index(string searchString, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + searchString;
        }


        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Employee_ID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        /*

        // GET: Employees/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Areas = await _context.Areas.ToListAsync();
            return View();
        }

        */

        public IActionResult Create() {

            ViewBag.Areas = new SelectList(_context.Areas.ToList(), "Area_ID", "Area_Name");

            return View(); 
        }

        // POST: Employees/Create
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Employee_ID,First_Name,Last_Name,Dni,Area_ID")] Employee employee)
        {
      

            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Areas = new SelectList(_context.Areas.ToList(), "Area_ID", "Area_Name");

            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Employee_ID,First_Name,Last_Name,Dni,Area_ID")] Employee employee)
        {
            if (id != employee.Employee_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Employee_ID))
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
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Employee_ID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Employee_ID == id);
        }
    }
}

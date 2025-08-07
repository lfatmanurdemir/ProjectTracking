using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectTracking.Core.Entities;
using ProjectTracking.Core.MyContext;
using ProjectTracking.DAL.Repository;

namespace ProjectTracking.Web.Controllers
{
    public class EmployeeProjectsController : Controller
    {
        private readonly IService<Project> _db;
        private readonly IService<Employee> _dbEmployee;
        private readonly AppDbContext _context;
        public EmployeeProjectsController(IService<Project> db, IService<Employee> dbEmployee, AppDbContext context)
        {
            _db = db;
            _dbEmployee = dbEmployee;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var data = _context.Projects.Include(x => x.Employees);


            return View(_context.Projects.Include(x => x.Employees));
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create()
        {
            var employee = await _dbEmployee.GetAllAsync();

            ViewBag.EmployeeDropdown = new SelectList(employee, "Id", "FullName");
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Create(Project projeObj, string employeeIds)
        {

            if (string.IsNullOrEmpty(employeeIds))
            {
                ModelState.AddModelError("Employees", "En az bir çalışan seçilmelidir.");
                var employee = await _dbEmployee.GetAllAsync();

                ViewBag.EmployeeDropdown = new SelectList(employee, "Id", "FullName");
                return View();
            }

            var employeeIdList = employeeIds.Split(',').Select(int.Parse).ToList();
            projeObj.Employees = new List<Employee>();

            foreach (var x in employeeIdList)
            {
                var employee = await _dbEmployee.GetByIdAsync(x);
                projeObj.Employees.Add(employee);
            }

            await _db.AddAsync(projeObj);

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(int id)
        {
            var projeObj = await _db.GetByIdAsync(id);

            return View(projeObj);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Project projeObj)
        {
            var projeDbObj = await _db.GetByIdAsync(projeObj.Id);

            projeDbObj.Description = projeObj.Description;
            projeDbObj.Title = projeObj.Title;
            projeDbObj.CompletionRate = projeObj.CompletionRate;
            projeDbObj.PriorityStatus = projeObj.PriorityStatus;

            await _db.UpdateAsync(projeDbObj);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Tamamla(int id)
        {
            var projeObj = await _db.GetByIdAsync(id);
            projeObj.CompletionStatus = true;
            projeObj.CompletionRate = 100;

            await _db.UpdateAsync(projeObj);

            return RedirectToAction("Index");
        }


        public async Task<ActionResult> Delete(int id)
        {
            var projeDbObj = await _db.GetByIdAsync(id);

            if (projeDbObj == null)
            {
                return NotFound();
            }
            await _db.RemoveAsync(projeDbObj);

            return RedirectToAction("Index");
        }
    }
}

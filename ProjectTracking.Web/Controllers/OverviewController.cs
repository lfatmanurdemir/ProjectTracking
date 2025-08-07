using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectTracking.Core.Entities;
using ProjectTracking.Core.MyContext;
using ProjectTracking.DAL.Repository;
using System.Linq;

namespace ProjectTracking.Web.Controllers
{
    public class OverviewController : Controller
    {
        private readonly IService<Project> _db;
        private readonly IService<Employee> _dbEmployee;
        private readonly AppDbContext _context;

        public OverviewController(IService<Project> db, IService<Employee> dbEmployee, AppDbContext context)
        {
            _db = db;
            _dbEmployee = dbEmployee;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var allProjects = await _db.GetAllAsync();
            var allEmployees = await _context.Employees.Include(e => e.Projects).ToListAsync();

            ViewBag.NumberOfProjects = allProjects.Count();
            ViewBag.CompletedProject = allProjects.Count(p => p.CompletionStatus);
            ViewBag.UnfinishedProject = allProjects.Count(p => !p.CompletionStatus);

            ViewBag.HighPriorityProjects = allProjects.Count(p => p.PriorityStatus == "Yüksek Öncelikli");
            ViewBag.MediumPriorityProjects = allProjects.Count(p => p.PriorityStatus == "Orta Öncelikli");
            ViewBag.LowPriorityProjects = allProjects.Count(p => p.PriorityStatus == "Düşük Öncelikli");

            ViewBag.SuccessfulAndHigh = allProjects.Count(p => p.CompletionStatus && p.PriorityStatus == "Yüksek Öncelikli");
            ViewBag.SuccessfulAndMedium = allProjects.Count(p => p.CompletionStatus && p.PriorityStatus == "Orta Öncelikli");
            ViewBag.SuccessfulAndLow = allProjects.Count(p => p.CompletionStatus && p.PriorityStatus == "Düşük Öncelikli");

            var employeeCompletedProjectCounts = allEmployees
                .Select(e => new
                {
                    Employee = e,
                    CompletedCount = e.Projects.Count(p => p.CompletionStatus)
                })
                .OrderByDescending(e => e.CompletedCount)
                .ToList();

            var topEmployee = employeeCompletedProjectCounts.FirstOrDefault();

            ViewBag.MostCompletedEmployeeInformation = topEmployee?.Employee.FullName ?? "Veri yok";
            ViewBag.NumberOfEmployeesWhoCompletedTheMostProjects = topEmployee?.CompletedCount ?? 0;

            return View();
        }

        public async Task<IActionResult> GeneralStatistic()
        {
            var allEmployees = await _context.Employees.Include(e => e.Projects).ToListAsync();
            var allProjects = await _context.Projects.Include(p => p.Employees).ToListAsync();

            var completedProjectDict = new Dictionary<int, int>();
            var uncompletedProjectDict = new Dictionary<int, int>();
            var totalProjectDict = new Dictionary<int, int>();

            foreach (var employee in allEmployees)
            {
                var employeeProjects = allProjects.Where(p => p.Employees.Any(e => e.Id == employee.Id)).ToList();

                int completed = employeeProjects.Count(p => p.CompletionStatus);
                int uncompleted = employeeProjects.Count(p => !p.CompletionStatus);
                int total = employeeProjects.Count;

                completedProjectDict[employee.Id] = completed;
                uncompletedProjectDict[employee.Id] = uncompleted;
                totalProjectDict[employee.Id] = total;
            }

            ViewBag.NumberOfCompletedProjects = completedProjectDict;
            ViewBag.NumberOfUncompletedProjects = uncompletedProjectDict;
            ViewBag.TotalNumberOfProjects = totalProjectDict;

            ViewBag.NumberOfProjects = allProjects.Count;
            ViewBag.NumberOfEmployee = allEmployees.Count;

            ViewBag.CompletedProjects = allProjects.Count(p => p.CompletionStatus);
            ViewBag.UnfinishedProject = allProjects.Count(p => !p.CompletionStatus);

            ViewBag.UnsuccessfulAndHigh = allProjects.Count(p => !p.CompletionStatus && p.PriorityStatus == "Yüksek Öncelikli");
            ViewBag.UnsuccessfulAndMedium = allProjects.Count(p => !p.CompletionStatus && p.PriorityStatus == "Orta Öncelikli");

            return View(allEmployees);
        }
    }
}

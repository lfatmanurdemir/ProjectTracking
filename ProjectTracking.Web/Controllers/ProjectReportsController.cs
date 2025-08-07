using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectTracking.Core.Entities;
using ProjectTracking.DAL.Repository;

namespace ProjectTracking.Web.Controllers
{
    public class ProjectReportsController : Controller
    {
        private readonly IService<Project> _db;
        private readonly IService<Employee> _dbEmployee;

        public ProjectReportsController(IService<Project> db, IService<Employee> dbEmployee)
        {
            _db = db;
            _dbEmployee = dbEmployee;
        }

        public ActionResult CompletedPriorityGroups()
        {
            return View();
        }

        public async Task<ActionResult> VisualizeCompletedStatusGroups()
        {
            var data = await PriorityGroupType();

            return Json(data);
        }

        public async Task<List<PrioritySituationAnalysis>> PriorityGroupType()
        {
            List<PrioritySituationAnalysis> cls = new List<PrioritySituationAnalysis>();

            cls = await _db.Where(x => x.CompletionStatus == true).GroupBy(p => p.PriorityStatus).Select(x => new PrioritySituationAnalysis
            {
                PrioritType = x.Key,
                NumberOfPriority = x.Count(),
            }).ToListAsync();

            return cls;
        }

        public ActionResult IncompletePriorityGroups()
        {
            return View();
        }

        public async Task<ActionResult> VisualizeIncompleteStatusGroups()
        {
            var data = await IncompletePriorityGroupType();
            return Json(data);
        }

        public async Task<List<PrioritySituationAnalysis>> IncompletePriorityGroupType()
        {
            List<PrioritySituationAnalysis> cls = new List<PrioritySituationAnalysis>();
            cls = await _db.Where(x => x.CompletionStatus == false).GroupBy(p => p.PriorityStatus).Select(x => new PrioritySituationAnalysis
            {
                PrioritType = x.Key,
                NumberOfPriority = x.Count(),
            }).ToListAsync();

            return cls;
        }

        public ActionResult GeneralProjectReports()
        {
            return View();
        }

        public async Task<ActionResult> LiveSupport()
        {
            return View(await _dbEmployee.GetAllAsync());
        }
    }
}

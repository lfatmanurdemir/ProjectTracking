using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectTracking.Core.Entities;
using ProjectTracking.DAL.Repository;
using ProjectTracking.DTO.EmployeeDTO;

namespace ProjectTracking.Web.Controllers
{
    public class EmployeeInformationsController : Controller
    {
        private readonly IService<Employee> _dbEmployee;
        private readonly IMapper _mapper;

        public EmployeeInformationsController(IService<Employee> dbEmployee, IMapper mapper)
        {
            _dbEmployee = dbEmployee;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index()
        {
            return View(await _dbEmployee.GetAllAsync());
        }

        public async Task<ActionResult> EmployeeCardAsync()
        {
            return View(await _dbEmployee.GetAllAsync());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EmployeeCreateDTO model, IFormFile empImage)
        {
            if (ModelState.IsValid)
            {
                var employee = _mapper.Map<Employee>(model);

                if (empImage != null)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", empImage.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await empImage.CopyToAsync(stream);
                    }

                    employee.Image = "/uploads/" + empImage.FileName;
                }

                await _dbEmployee.AddAsync(employee);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var employee = await _dbEmployee.GetByIdAsync(id.Value);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        public async Task<ActionResult> EditAsync(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var employee = await _dbEmployee.GetByIdAsync(id.Value);

            if (employee == null)
            {
                return NotFound();
            }

            var employeeUpdateDTO = _mapper.Map<EmployeeUpdateDTO>(employee);
            return View(employeeUpdateDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EmployeeUpdateDTO model, IFormFile empImage)
        {
            if (ModelState.IsValid)
            {
                var employee = await _dbEmployee.GetByIdAsync(model.Id);

                if (employee == null)
                {
                    return NotFound();
                }

                employee.Email = model.Email;
                employee.Password = model.Password;
                employee.Role = model.Role;
                employee.FullName = model.FullName;

                if (empImage != null)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", empImage.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await empImage.CopyToAsync(stream);
                    }

                    employee.Image = "/uploads/" + empImage.FileName;
                }

                employee.IDNumber = model.IDNumber;
                employee.Department = model.Department;
                employee.Position = model.Position;
                employee.PositionDescription = model.PositionDescription;
                employee.PhoneNumber = model.PhoneNumber;
                employee.Address = model.Address;
                employee.MaritalStatus = model.MaritalStatus;
                employee.EmergencyContact = model.EmergencyContact;
                employee.UpdatedDate = DateTime.Now;

                await _dbEmployee.UpdateAsync(employee);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var t = await _dbEmployee.GetByIdAsync(id.Value);

            await _dbEmployee.RemoveAsync(t);

            return RedirectToAction("Index");
        }
    }
}

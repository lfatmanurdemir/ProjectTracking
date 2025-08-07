using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectTracking.Core.Entities;
using ProjectTracking.DAL.Repository;
using ProjectTracking.DTO.EmployeeDTO;
using System.Security.Claims;

namespace ProjectTracking.Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly IService<Employee> _db;
        public LoginController(IService<Employee> db)
        {
            _db = db;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Index(Employee employee)
        {
            var information = await _db.Where(x => x.Email == employee.Email && x.Password == employee.Password).FirstOrDefaultAsync();

            if (information != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, employee.Email),
                };

                if (employee.Email == "admin@admin.com")
                {
                    claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, "User"));
                }

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToAction("Index", "Overview");
            }
            else
            {
                ViewBag.ErrorMessage = "Geçersiz email veya şifre!";
                return View();
            }
        }

        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Register(EmployeeRegisterDTO registerDTO)
        {
            var employee = new Employee
            {
                Email = registerDTO.Email,
                Password = registerDTO.Password,
                FullName = registerDTO.FullName
            };

            var existingEmployee = await _db.Where(x => x.Email == registerDTO.Email).FirstOrDefaultAsync();

            if (existingEmployee != null)
            {
                ViewBag.ErrorMessage = "Bu email adresi zaten kayıtlı!";
                return View();
            }

            await _db.AddAsync(employee);

            return RedirectToAction("Index", "Overview");
        }
    }
}

using ProjectTracking.Core.Entities;
using ProjectTracking.Core.MyContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTracking.Core.Seed
{
    public static class EmployeeSeed
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (!context.Employees.Any())
            {
                var employees = new List<Employee>
            {
                new Employee
                {

                    Email = "admin@admin.com",
                    Password = "Admin1!",
                    Role = "Admin",
                    FullName = "Admin Admin",
                    Image = "/uploads/user-icon.png",
                    IDNumber = "12345678900",
                    Department = "Bilgi Teknolojileri",
                    Position = "Yazılım Geliştirici",
                    PositionDescription = "Full Stack Developer",
                    PhoneNumber = "0 532 122 45 67",
                    Address = "İzmir, Türkiye",
                    MaritalStatus = "Bekar",
                    EmergencyContact = "Kardeş",
                    EmergencyContactID = "10957654321",
                    EmergencyContactFullName = "Ahmet Batur",
                    EmergencyContactPhone = "0 532 165 43 21",
                    DateOfBirth = new DateTime(1995, 5, 15),
                    JoiningDate = new DateTime(2022, 3, 1)
                },
               
                new Employee
                {
                    Email = "fatma.demir@gmail.com",
                    Password = "12345",
                    Role = "User",
                    FullName = "Fatma Demir",
                    Image = "/uploads/user-icon.png",
                    IDNumber = "23456789012",
                    Department = "İnsan Kaynakları",
                    Position = "İK Uzmanı",
                    PositionDescription = "Personel işleri ve işe alım",
                    PhoneNumber = "0 533 234 56 78",
                    Address = "İstanbul, Türkiye",
                    MaritalStatus = "Evli",
                    EmergencyContact = "Anne",
                    EmergencyContactID = "21098765432",
                    EmergencyContactFullName = "Tuğba Demir",
                    EmergencyContactPhone = "0 533 876 54 32",
                    DateOfBirth = new DateTime(1990, 9, 20),
                    JoiningDate = new DateTime(2021, 7, 15)
                }
            };

                await context.Employees.AddRangeAsync(employees);
                await context.SaveChangesAsync();
            }
        }
    }
}

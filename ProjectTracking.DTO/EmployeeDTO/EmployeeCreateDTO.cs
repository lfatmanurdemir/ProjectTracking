using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTracking.DTO.EmployeeDTO
{
    public class EmployeeCreateDTO
    {
        [DisplayName("E-POSTA")]
        public string Email { get; set; }

        [DisplayName("ŞİFRE")]
        [StringLength(25, ErrorMessage = "Maximum uzunluk 25 karakterden fazla olamaz")]
        public string Password { get; set; }

        [DisplayName("YETKİ")]
        [StringLength(15, ErrorMessage = "Maximum uzunluk 15 karakterden fazla olamaz")]
        public string Role { get; set; }
        [DisplayName("AD SOYAD")]
        [StringLength(50, ErrorMessage = "Maximum uzunluk 50 karakterden fazla olamaz")]
        public string FullName { get; set; }

        [DisplayName("TC KİMLİK NO")]
        [StringLength(15, ErrorMessage = "Maximum uzunluk 15 karakterden fazla olamaz")]
        public string IDNumber { get; set; }

        [DisplayName("DEPARTMANI")]
        public string Department { get; set; }

        [DisplayName("GÖREVİ")]
        [StringLength(25, ErrorMessage = "Maximum uzunluk 25 karakterden fazla olamaz")]
        public string Position { get; set; }

        [DisplayName("AÇIKLAMA")]
        public string PositionDescription { get; set; }

        [DisplayName("TELEFON NUMARASI")]
        [StringLength(15, ErrorMessage = "Maximum uzunluk 15 karakterden fazla olamaz")]
        public string PhoneNumber { get; set; }

        [DisplayName("ADRES BİLGİLERİ")]
        public string Address { get; set; }

        [DisplayName("MEDENİ HAL")]
        [StringLength(25, ErrorMessage = "Maximum uzunluk 25 karakterden fazla olamaz")]
        public string MaritalStatus { get; set; }

        [DisplayName("YAKINLIK BİLGİSİ")]
        [StringLength(25, ErrorMessage = "Maximum uzunluk 25 karakterden fazla olamaz")]
        public string EmergencyContact { get; set; }

        [DisplayName("YAKIN TC NO")]
        [StringLength(25, ErrorMessage = "Maximum uzunluk 25 karakterden fazla olamaz")]
        public string EmergencyContactID { get; set; }

        [DisplayName("YAKIN AD SOYAD")]
        [StringLength(25, ErrorMessage = "Maximum uzunluk 25 karakterden fazla olamaz")]
        public string EmergencyContactFullName { get; set; }

        [DisplayName("YAKIN TELEFONU")]
        [StringLength(25, ErrorMessage = "Maximum uzunluk 25 karakterden fazla olamaz")]
        public string EmergencyContactPhone { get; set; }

        [DisplayName("DOĞUM TARİHİ")]
        public DateTime DateOfBirth { get; set; }

        [DisplayName("İŞE GİRİŞ TARİHİ")]
        public DateTime? JoiningDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTracking.Core.Entities
{
    public class Project : BaseEntity
    {
        [DisplayName("PROJE BAŞLIĞI")]
        [StringLength(150, ErrorMessage = "Maximum uzunluk 150 karakterden fazla olamaz")]
        public string Title { get; set; }

        [DisplayName("PROJE AÇIKLAMASI")]
        public string Description { get; set; }

        [DisplayName("OLUŞTURMA TARİHİ")]
        public DateTime CreateDate { get; set; }

        [DisplayName("ÖNCELİK DURUMU")]
        [StringLength(25, ErrorMessage = "Maximum uzunluk 25 karakterden fazla olamaz")]
        public string PriorityStatus { get; set; }

        [DisplayName("TAMAMLANMA ORANI")]
        public int CompletionRate { get; set; }

        [DisplayName("TAMAMLANMA TARİHİ")]
        public DateTime? CompletionDate { get; set; }

        [DisplayName("TAMAMLANMA DURUMU")]
        public bool CompletionStatus { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}

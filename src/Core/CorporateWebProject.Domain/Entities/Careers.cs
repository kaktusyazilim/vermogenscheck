using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class Careers:EntityBase
    {
        public string CompanyGuid { get; set; } = string.Empty;
        public string JobGuid { get; set; } = string.Empty;

        [Required(ErrorMessage ="İsim kısmı zorunludur")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Soyisim kısmı zorunludur")]

        public string Surname { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Mail { get; set; }=string.Empty;
        public string MaritalStatus { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public int ChildrenCount { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Job { get; set; } = string.Empty;
        public string EducationStatus { get; set; } = string.Empty;
        public string JobHistory { get; set; } = string.Empty;
        public string MilitaryStatus { get; set; } = string.Empty;
        public string DriverStatus { get; set; } = string.Empty;
        public string HealthStatus { get; set; } = string.Empty;
        public string ConvictStatus { get; set; } = string.Empty;
        public string CoursesAttended { get; set; } = string.Empty;
        public string Reference { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public bool IsGeneral { get; set; }
    }
}

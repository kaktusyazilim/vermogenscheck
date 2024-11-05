using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Application.Dto.Authorization
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string ItemGuid { get; set; } = string.Empty;
        public int Type { get; set; }
        public int SchoolId { get; set; }
        public string School { get; set; } = string.Empty;
        public string SchoolCode { get; set; }=string.Empty;
        public int SchoolClassId { get; set; }
        public string SchoolClass { get; set; } = string.Empty;
        public int CityId { get; set; }
        public string City { get; set; } = string.Empty;
        public int DistrictId { get; set; }
        public string District { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Mail { get; set; } = string.Empty;
        public int Gender { get; set; }
        public string Photo { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public bool IsVertification { get; set; }
        public bool IsConfirmation { get; set; }
    }
}

using CorporateWebProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Application.Dto.SchoolFilter
{
    public class SchoolFilterDTO
    {
        public Categories CurrentProgram { get; set; } 
        public SubCategories CurrentSubProgram { get; set; } 
        public SubCategories CurrentLanguage { get; set; }
        public SubCategories CurrentCountry { get; set; } 
        public LowestCategory CurrentCity { get; set; }
    }
}

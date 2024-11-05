using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class Comments:EntityBase
    {
        public int Queue { get; set; }
        public string Title { get; set; }

        public string Comment { get; set; }
        public string SubComment { get; set; }
        public string FilePath { get; set; }
        public string CountryGuid { get; set; }
        public string CityGuid { get; set; }
        public string VideoPath { get; set; }
    }
}

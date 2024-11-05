using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class Locations:EntityBase
    {
        public string City { get; set; }
        public string District { get; set; }
        public string Town { get; set; }
        public string Neighborhood { get; set; }
        public string PostalCode { get; set; }
    }
}

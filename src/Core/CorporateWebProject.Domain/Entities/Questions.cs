using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class Questions:EntityBase
    {
        public string SurveyGuid { get; set; }
        public int Queue { get; set; }
        public string Name { get; set; }
    }
}

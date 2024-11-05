using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class Answers:EntityBase
    {
        public string SurveyGuid { get; set; } = string.Empty;
        public string QuestionGuid { get; set; } = string.Empty;
        public string TargetQuestionGuid { get; set; } = string.Empty;

        public int Queue { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
    }
}

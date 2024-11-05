﻿using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class Personnels:EntityBase
    {
        public int LangId { get; set; }
        public int Queue { get; set; }
        public string NameSurname { get; set; } = string.Empty;
        public string Job { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;
    }
}
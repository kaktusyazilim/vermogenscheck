﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Application.Dto.New
{
    public class NewDTO
    {
        public string Title { get; set; }=string.Empty;
        public string Description { get; set; }=string.Empty;
        public List<NewItemDTO> News { get; set; } = new();
    }
}

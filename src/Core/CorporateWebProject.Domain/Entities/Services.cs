﻿using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class Services:EntityBase
    {
        public int LangId { get; set; }
        public int TypeId { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int Queue { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string FullDescription { get; set; } = string.Empty;
        public string MetaDescription { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;
        public string FriendlyUrl { get; set; } = string.Empty;
        public bool IsHomePage { get; set; }

    }
}

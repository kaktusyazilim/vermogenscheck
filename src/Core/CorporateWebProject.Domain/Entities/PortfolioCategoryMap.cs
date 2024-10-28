using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class PortfolioCategoryMap : EntityBase
    {
        public string PortfolioFriendlyUrl { get; set; } = string.Empty;
        public int PortfolioId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public int TypeId { get; set; }
    }
}

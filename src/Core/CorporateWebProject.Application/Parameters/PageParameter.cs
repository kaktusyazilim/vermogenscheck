using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Application.Parameters
{
    public class PageParameter
    {
        private decimal _totalPage = 0.0m;
        private decimal _skip = 0.0m;
        private decimal _quantity = 0.0m;

        public int CurrentPage { get; set; }
        public decimal TotalPage { get { return _totalPage; } set { _totalPage = Math.Ceiling(value / this.Quanity); } }
        public decimal Skip { get { return _skip; } set { _skip = (this.CurrentPage - 1) * this.Quanity; } }
        public decimal Quanity { get { return _quantity; } set { _quantity = value; } }


    }
}

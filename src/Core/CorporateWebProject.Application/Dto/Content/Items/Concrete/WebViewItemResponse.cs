using CorporateWebProject.Application.Dto.Content.Items.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Application.Dto.Content.Items.Concrete
{
    public class WebViewItemResponse : IBaseItem
    {
        public string Url { get; set; } = string.Empty;
    }
}

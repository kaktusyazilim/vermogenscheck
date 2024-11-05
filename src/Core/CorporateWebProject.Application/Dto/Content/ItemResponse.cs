using CorporateWebProject.Application.Dto.Content.Items.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Application.Dto.Content
{
    public class ItemResponse
    {
        public IBaseItem? Content { get; set; } 
    }
}

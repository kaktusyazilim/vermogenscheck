using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Application.Dto.New
{
    public class NewResponseDTO
    {
        public List<NewSourceDTO> Sources { get; set; } = new();
        public List<NewItemDTO> News { get; set; } = new();
    }
}

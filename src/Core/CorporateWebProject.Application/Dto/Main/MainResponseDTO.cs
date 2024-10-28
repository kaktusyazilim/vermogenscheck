using CorporateWebProject.Application.Dto.Content;
using CorporateWebProject.Application.Dto.New;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Application.Dto.Main
{
    public class MainResponseDTO
    {
        public List<SliderDTO> Sliders { get; set; } = new();
        public NewItemDTO Featured { get; set; } = new();
        public List<NewItemDTO> BreakingNews { get; set; } = new();
        public List<SubCategoryResponse> PopularContent { get; set; } = new();
        public List<SubCategoryResponse> AllContent { get; set; } = new();

    }
}

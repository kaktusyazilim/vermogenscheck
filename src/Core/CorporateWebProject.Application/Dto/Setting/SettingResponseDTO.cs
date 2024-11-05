using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Application.Dto.Setting
{
    public class SettingResponseDTO
    {
        public string MainColor { get; set; } = string.Empty;
        public string PrimaryColor { get; set; } = string.Empty;
        public string About { get; set; } = string.Empty;
        public string Instagram { get; set; } = string.Empty;
        public string Facebook { get; set; } = string.Empty;
        public string Pinterest { get; set; } = string.Empty;
        public string LinkedIn { get; set; }=string.Empty;
        public string Twitter { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;
        public string DarkLogo { get; set; } = string.Empty;
        public string WhiteLogo { get; set; } = string.Empty;
    }
}

using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class Settings:EntityBase
    {
        public int LangId { get; set; }
        public string Url { get; set; } = string.Empty;
        public string SiteName { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string WebDescription { get; set; } = string.Empty;
        public string Phone1 { get; set; } = string.Empty;
        public string Phone2 { get; set; } = string.Empty;
        public string Mail { get; set; } = string.Empty;
        public string Faks { get; set; } = string.Empty;
        public string CompanyExcel { get; set; } = string.Empty;

        public string DarkLogo { get; set; } = string.Empty;
        public string WhiteLogo { get; set; } = string.Empty;
        public string Favicon { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string GoogleMap { get; set; } = string.Empty;
        public string GoogleMapShortLink { get; set; } = string.Empty;

        public string MetaDescription { get; set; } = string.Empty;
        public string WorkHours { get; set; } = string.Empty;
        public string Vision { get; set; } = string.Empty;
        public string VisionPhoto { get; set; } = string.Empty;
        public string Mission { get; set; } = string.Empty;
        public string MissionPhoto { get; set; } = string.Empty;
        public string HeaderScripts { get; set; } = string.Empty;
        public string BodyScripts { get; set; } = string.Empty;
        public string FooterScripts { get; set; } = string.Empty;
        public string AboutVideoLink { get; set; } = string.Empty;
        public string AboutPhoto { get; set; } = string.Empty;
        public string AboutTitle { get; set; } = string.Empty;
        public string AboutDescription { get; set; } = string.Empty;
        public string BreadcrumbImage { get; set; } = string.Empty;
        public string SeperatorImage { get; set; } = string.Empty;
        public string GoogleSiteVeritification { get; set; } = string.Empty;
        public string YandexVeritification { get; set; } = string.Empty;
        public string FacebookDomainVeritification { get; set; } = string.Empty;
        public string FacebookId { get; set; } = string.Empty;
        public string TwitterId { get; set; } = string.Empty;


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
    public class ExcelModel
    {
        public string Id { get; set; } = string.Empty;
        public string CreatedTime { get; set; } = string.Empty;
        public string AdId { get; set; } = string.Empty;
        public string AdName { get; set; } = string.Empty;
        public string AdsetId { get; set; } = string.Empty;
        public string AdsetName { get; set; } = string.Empty;
        public string CampaignId { get; set; } = string.Empty;
        public string CampaignName { get; set; } = string.Empty;
        public string FormId { get; set; } = string.Empty;
        public string FormName { get; set; } = string.Empty;
        public bool IsOrganic { get; set; }
        public string Platform { get; set; } = string.Empty;
        public string DanismanlikAlmakIsteginizKonuNedir { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public string LeadStatus { get; set; } = string.Empty;
    }
}

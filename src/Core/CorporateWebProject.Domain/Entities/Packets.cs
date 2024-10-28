using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Domain.Entities
{
  public  class Packets:EntityBase
    {
        public string PacketGuid { get; set; } = string.Empty;
        public int ServiceId { get; set; }
        public int Queue { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Subname { get; set; } = string.Empty;
        public string Properties { get; set; } = string.Empty;
        public string Price { get; set; } = string.Empty;
        public string HighlightFeature { get; set; } = string.Empty;

    }
}

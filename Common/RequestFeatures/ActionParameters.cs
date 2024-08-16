using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.RequestFeatures
{
    public class ActionParameters : RequestParameters
    {
        public DateTime? MinDate { get; set; } = DateTime.UtcNow.Date;
        public DateTime? MaxDate { get; set; } = DateTime.MaxValue;
        public int PlaceID { get; set; }
        public string? Search { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.RequestFeatures
{
    public class ActionParameters : RequestParameters
    {
        public DateTime? MinDate { get; set; } = DateTime.MinValue;
        public DateTime? MaxDate { get; set; } = DateTime.MaxValue;
        public string? Search { get; set; }
    }
}

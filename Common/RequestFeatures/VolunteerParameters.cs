using BloodDonationApp.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Common.RequestFeatures
{
    public class VolunteerParameters : RequestParameters
    {
        public DateTime? DateFreeFrom { get; set; } = DateTime.MaxValue;
        public DateTime? DateFreeTo { get; set; } = DateTime.MinValue;
        public DateTime? MinDateOfBirth { get; set; } = DateTime.MaxValue;
        public Sex? Sex { get; set; }
        public int RedCrossID { get; set; }
        public string? Search { get; set; }
        public VolunteerParameters() => OrderBy = "DateOfBirth";
    }
}

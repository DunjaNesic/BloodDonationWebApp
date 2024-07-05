using BloodDonationApp.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.RequestFeatures
{
    public class DonorParameters : RequestParameters
    {
        public BloodType? BloodType { get; set; }
        public bool? IsActive { get; set; }
        public DateTime NextDonationDate { get; set; } = DateTime.MaxValue;
        public Sex? Sex { get; set; }
        public int PlaceID { get; set; }
        public string? Search { get; set; }
        public DonorParameters() => OrderBy = "IsActive";
    }
}

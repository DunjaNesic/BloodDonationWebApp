using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BloodDonationApp.Domain.DomainModel;


namespace BloodDonationApp.DataTransferObject.Donor
{
    public class GetDonorDTO
    {
        public string JMBG { get; set; } = string.Empty;
        public string DonorFullName { get; set; } = string.Empty;
        public string DonorEmailAddress { get; set; } = string.Empty;
        public BloodType BloodType { get; set; }
        public string PlaceName { get; set; } = string.Empty;
        public IsActive IsActive { get; set; }

    }
}

using BloodDonationApp.DataTransferObject.Donor;
using BloodDonationApp.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.Presentation.Mappers
{
    public static class DonorMapper
    {
        public static GetDonorDTO ToDto(this Donor donor) => new()
        {
            JMBG = donor.JMBG,
            DonorFullName = donor.DonorFullName,
            DonorEmailAddress = donor.DonorEmailAddress,
            PlaceName = donor.Place?.PlaceName ?? "Nepoznat grad",
            BloodType = donor.BloodType,
            IsActive = donor.IsActive
        };
    }
}

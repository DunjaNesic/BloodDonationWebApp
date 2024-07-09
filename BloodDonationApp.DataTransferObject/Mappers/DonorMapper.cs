using BloodDonationApp.DataTransferObject;
using BloodDonationApp.DataTransferObject.Donors;
using BloodDonationApp.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataTransferObject.Mappers
{
    public class DonorMapper : IMapperCustom<GetDonorDTO, Donor>
    {
        public GetDonorDTO ToDto(Donor donor) => new()
        {
            JMBG = donor.JMBG,
            DonorFullName = donor.DonorFullName,
            DonorEmailAddress = donor.DonorEmailAddress,
            PlaceName = donor.Place?.PlaceName ?? "Nepoznat grad",
            BloodType = donor.BloodType,
            IsActive = donor.IsActive,
            LastDonationDate = donor.LastDonationDate,
            CallsToDonate = donor.CallsToDonate?.Select(call => new CallsToDonorDTO{
                AcceptedTheCall = call.AcceptedTheCall,
                ShowedUp = call.ShowedUp
            }).ToList()
        };
        public Donor FromDto(GetDonorDTO source)
        {
            throw new NotImplementedException();
        }
    }
}

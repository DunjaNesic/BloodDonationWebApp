using BloodDonationApp.DataTransferObject.Donors;
using BloodDonationApp.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataTransferObject.Mappers
{
    public static class CallToDonateMapper
    {
        public static CallsToDonorDTO ToDto(this CallToDonate callToDonate) => new() {
            AcceptedTheCall = callToDonate.AcceptedTheCall,
            ShowedUp = callToDonate.ShowedUp
        };
    }
}

using BloodDonationApp.DataTransferObject.Action;
using BloodDonationApp.DataTransferObject.Donors;
using BloodDonationApp.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataTransferObject.Mappers
{
    public class CallToDonateMapper : IMapperCustom<CallsToDonorDTO, CallToDonate>
    {
        public CallsToDonorDTO ToDto(CallToDonate callToDonate) => new() {
            AcceptedTheCall = callToDonate.AcceptedTheCall,
            ShowedUp = callToDonate.ShowedUp
        };

        public CallToDonate FromDto(CallsToDonorDTO source)
        {
            throw new NotImplementedException();
        }

    }
}

using BloodDonationApp.DataTransferObject.Officials;
using BloodDonationApp.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataTransferObject.Mappers
{
    public class OfficialMapper : IMapperCustom<GetOfficialDTO, Official>
    {
        public Official FromDto(GetOfficialDTO official)
        {
            throw new NotImplementedException();
        }

        public GetOfficialDTO ToDto(Official official) => new()
        {
            OfficialID = official.OfficialID,
            OfficialFullName = official.OfficialFullName,
            UserID = official.UserID,
        };
    }
}

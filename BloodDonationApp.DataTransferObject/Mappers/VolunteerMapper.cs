using BloodDonationApp.DataTransferObject.Action;
using BloodDonationApp.DataTransferObject.Volunteers;
using BloodDonationApp.Domain.DomainModel;

namespace BloodDonationApp.DataTransferObject.Mappers
{
    public class VolunteerMapper : IMapperCustom<GetVolunteerDTO, Volunteer>
    {
        public GetVolunteerDTO ToDto(Volunteer volunteer) => new()
        {
            VolunteerID = volunteer.VolunteerID,
            VolunteerFullName = volunteer.VolunteerFullName,
            DateFreeFrom = volunteer.DateFreeFrom,
            DateFreeTo = volunteer.DateFreeTo,
            PlaceName = volunteer.RedCross?.InstitutionName ?? "Nepoznata institucija volontiranja",
            ListOfActions = (volunteer.ListOfActions ?? Enumerable.Empty<TransfusionAction>())
            .Select(action => new GetTransfusionActionDTO
            {
                ActionName = action.ActionName,
                ActionDate = action.ActionDate,
                ActionTimeFromTo = action.ActionTimeFromTo ?? string.Empty,
            }).ToList()
        };
        public Volunteer FromDto(GetVolunteerDTO source)
        {
            throw new NotImplementedException();
        }

    }
}

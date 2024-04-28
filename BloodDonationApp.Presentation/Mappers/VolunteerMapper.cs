using BloodDonationApp.DataTransferObject.Action;
using BloodDonationApp.DataTransferObject.Volunteer;
using BloodDonationApp.Domain.DomainModel;

namespace BloodDonationApp.Presentation
{
    public static class VolunteerMapper
    {
        public static GetVolunteerDTO ToDto(this Volunteer volunteer) => new()
        {
            VolunteerFullName = volunteer.VolunteerFullName,
            DateFreeFrom = volunteer.DateFreeFrom,
            DateFreeTo = volunteer.DateFreeTo,
            PlaceName = volunteer.Place?.PlaceName ?? "Nepoznat grad",
            RowVersion = volunteer.RowVersion,
            ListOfActions = volunteer.ListOfActions?.Select(action =>
                new GetTransfusionActionDTO
                {
                    ActionName = action.ActionName,
                    ActionDate = action.ActionDate,
                    ActionTimeFromTo = action.ActionTimeFromTo,
                }).ToList()
        };

        public static Volunteer FromDto(this CreateVolunteerDTO dto) => new()
        {
            VolunteerFullName = dto.VolunteerFullName,
            DateFreeFrom = dto.DateFreeFrom,
            DateFreeTo = dto.DateFreeTo,
            DateOfBirth = dto.DateOfBirth,
            PlaceID = dto.PlaceID,
            VolunteerEmailAddress = dto.VolunteerEmailAddress,
        };

        public static Volunteer FromDto(this UpdateVolunteerDTO dto) => new()
        {
            RowVersion = dto.RowVersion ?? throw new InvalidOperationException("RowVersion cannot be null"),
            VolunteerFullName = dto.VolunteerFullName ?? string.Empty,
            DateFreeFrom = dto.DateFreeFrom,
            DateFreeTo = dto.DateFreeTo,
            DateOfBirth = dto.DateOfBirth,
            PlaceID = dto.PlaceID,
            VolunteerEmailAddress = dto.VolunteerEmailAddress ?? string.Empty,
        };
    }
}

using BloodDonationApp.Domain.DomainModel;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BloodDonationApp.BusinessLogic.ServerSideValidation
{
    public class VolunteerValidation
    {

        public List<string> Validate(Volunteer volunteer)
        {
            if (volunteer == null)
                throw new ArgumentNullException(nameof(volunteer));

            var errorMessages = new List<string>();

            errorMessages.AddRange(ValidateField(volunteer.VolunteerFullName, "Ime i prezime volontera", ValidateVolunteerFullName));
            errorMessages.AddRange(ValidateField(volunteer.DateFreeFrom.ToString(), "Datum od", ValidateDateFreeFrom));
            errorMessages.AddRange(ValidateField(volunteer.DateFreeTo.ToString(), "Datum do", ValidateDateFreeTo));
            errorMessages.AddRange(ValidateField(volunteer.DateOfBirth.ToString(), "Datum rodjenja volontera", ValidateDateOfBirth));
            errorMessages.AddRange(ValidateField(volunteer.RedCrossID.ToString(), "Institucija volontera", ValidatePlaceID));

            return errorMessages;
        }

        private List<string> ValidateField(string? fieldValue, string fieldName, Func<string, string?> validationFunc)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(fieldValue))
            {
                errors.Add($"Morate proslediti vrednost za {fieldName}");
            }
            else
            {
                var validationMessage = validationFunc(fieldValue);
                if (validationMessage != null)
                {
                    errors.Add(validationMessage);
                }
            }
            return errors;
        }

        private string? ValidateVolunteerFullName(string volunteerFullName) {

            if (string.IsNullOrWhiteSpace(volunteerFullName))
                return "Morate proslediti vrednost za naziv volontera";

            Regex regex = new Regex(@"^[a-zA-Z\s]+$");

            if (regex.IsMatch(volunteerFullName) == false)
                return "Ime mora biti napisano iskljucivo slovima, bez brojeva";

            return null;
        }

        private string? ValidateVolunteerAddress(string volunteerEmail)
        {

            if (string.IsNullOrWhiteSpace(volunteerEmail))
                return "Morate proslediti vrednost za email volontera";

            Regex regex = new Regex(@"^[a-zA-Z\s]+$");

            return null;
        }

        private string? ValidateDateFreeFrom(string dateFreeFrom) {
            if (string.IsNullOrEmpty(dateFreeFrom))
                return "Morate proslediti vrednost za datum od";

            return null;
        }

        private string? ValidateDateFreeTo(string dateFreeTo)
        {
            if (string.IsNullOrEmpty(dateFreeTo))
                return "Morate proslediti vrednost za datum do";

            return null;
        }

        private string? ValidateDateOfBirth(string dateOfBirth)
        {
            if (string.IsNullOrEmpty(dateOfBirth))
                return "Morate proslediti vrednost za datum rodjenja";

            return null;
        }
             private string? ValidatePlaceID(string placeID)
        {
            if (string.IsNullOrEmpty(placeID))
                return "Morate proslediti vrednost za mesto rodjenja volontera";

            return null;
        }
    }
}

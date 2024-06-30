using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataTransferObject.Volunteers
{
    public abstract class VolunteerForManipulationDTO : IValidatableObject
    {
        [MinLength(3, ErrorMessage = "Ime volontera mora da sadrzi najmanje 3 slova")]
        [MaxLength(42, ErrorMessage = "Ime i prezime volontera ne sme da prelazi 42 slova")]
        public string VolunteerFullName { get; set; } = string.Empty;
        public required string Username { get; set; }
        public required string Password { get; set; }

        [EmailAddress(ErrorMessage = "Neispravan format email adrese")]
        public string VolunteerEmailAddress { get; set; } = string.Empty;
        public DateTime DateFreeFrom { get; set; }
        public DateTime DateFreeTo { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int PlaceID { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> validations = new List<ValidationResult>();

            if (!char.IsUpper(VolunteerFullName[0]))
            {
                validations.Add(new ValidationResult("Ime volontera mora da pocne velikim slovom"));
            }
            if (DateFreeFrom >= DateFreeTo)
            {
                validations.Add(new ValidationResult("Datum završetka angažmana mora biti posle datuma početka"));
            }

            var currentDate = DateTime.Now;
            var age = currentDate.Year - DateOfBirth.Year;

            if (age < 0 || age > 142) 
            {
                validations.Add(new ValidationResult("Neispravan datum rođenja"));
            }

            return validations;
        }
    }
}

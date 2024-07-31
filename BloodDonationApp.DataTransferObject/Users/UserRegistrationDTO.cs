using BloodDonationApp.Domain.DomainModel;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataTransferObject.Users
{
    public class UserRegistrationDTO
    {
        public required string JMBG { get; set; }
        public required string DonorFullName { get; set; }
        public string? Email { get; set; }

        [Required]

        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string? ConfirmPassword { get; set; }
        public Sex Sex { get; set; }
        public BloodType BloodType { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastDonationDate { get; set; }
        public int PlaceID { get; set; }

    }
}

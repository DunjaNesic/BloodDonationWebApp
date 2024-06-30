using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.Domain.DomainModel
{
    public class RedCross
    {
        [Key]
        public int RedCrossID { get; set; }
        public required string InstitutionName { get; set; }
        public int PlaceID { get; set; }
        public Place Place { get; set; } = null!;
    }
}

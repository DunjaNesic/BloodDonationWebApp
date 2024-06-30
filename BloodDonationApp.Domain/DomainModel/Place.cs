using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.Domain.DomainModel
{
    public class Place
    {
        [Key]
        public int PlaceID { get; set; }
        public required string PlaceName { get; set; }
        public override string ToString()
        {
            return PlaceName;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Place otherPlace = (Place)obj;

            return PlaceID == otherPlace.PlaceID;
        }

        public override int GetHashCode()
        {
            return PlaceID.GetHashCode();
        }
    }
}

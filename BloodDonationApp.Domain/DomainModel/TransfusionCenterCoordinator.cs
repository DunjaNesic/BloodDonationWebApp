using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.Domain.DomainModel
{
    public class TransfusionCenterCoordinator
    {
        [Key]
        public int CoordinatorID { get; set; }
        public string CoordinatorFullName { get; set; } = string.Empty;
        public string CoordinatorCode { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public override bool Equals(object? obj)
        {
            return obj is TransfusionCenterCoordinator coordinator &&
                   CoordinatorCode == coordinator.CoordinatorCode &&
                   Password == coordinator.Password;
        }
        public override int GetHashCode()
        {
            int hashCode = 1736740912;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(CoordinatorCode);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Password);
            return hashCode;
        }
    }
}

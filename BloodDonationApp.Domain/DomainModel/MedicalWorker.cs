using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.Domain.DomainModel
{
    public class MedicalWorker : Official
    {
        public string? HealthcareOccupation { get; set; }
    }
}

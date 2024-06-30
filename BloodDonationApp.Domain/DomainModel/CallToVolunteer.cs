using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.Domain.DomainModel
{
    public class CallToVolunteer
    {
        public int VolunteerID { get; set; }
        public Volunteer Volunteer { get; set; } = null!;
        public int ActionID { get; set; }
        public TransfusionAction Action { get; set; } = null!;
        public bool AcceptedTheCall { get; set; }
        public bool ShowedUp { get; set; }

    }
}

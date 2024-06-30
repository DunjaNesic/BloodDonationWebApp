using BloodDonationApp.Domain.ResponsesModel.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.Domain.ResponsesModel.ConcreteResponses.Volunteer
{
    public sealed class VolunteerUnprocessableEntityResponse : ApiUnprocessableEntityResponse
    {
        public VolunteerUnprocessableEntityResponse() : base("Volonter koga pokusavate da kreirate nije prosao nasu validaciju")
        {
        }
    }
}

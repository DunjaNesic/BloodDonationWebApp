using BloodDonationApp.Domain.ResponsesModel.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.Domain.ResponsesModel.ConcreteResponses.Volunteer
{
    public sealed class VolunteerNotFoundResponse : ApiNotFoundResponse
    {
        public VolunteerNotFoundResponse() : base($"Volonter koga trazite ne postoji u našoj bazi volontera")
        {
        }
    }
}

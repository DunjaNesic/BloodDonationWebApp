using BloodDonationApp.Domain.ResponsesModel.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.Domain.ResponsesModel.ConcreteResponses.Volunteer
{
    public sealed class VolunteerBadRequestResponse : ApiBadRequestResponse
    {
        public VolunteerBadRequestResponse(string partialName) : base($"Ne mozemo da obradimo Vaš zahtev i pronadjemo volontera sa {partialName} u imenu/prezimenu")
        {
        }
    }
}

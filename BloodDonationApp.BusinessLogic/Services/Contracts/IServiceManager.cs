using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.BusinessLogic.Services.Contracts
{
    public interface IServiceManager
    {
        IActionService ActionService { get; }
        IDonorService DonorService { get; }
        IPlaceService PlaceService { get; }
        IQuestionnaireService QuestionnaireService { get; }
        IQuestionService QuestionService { get; }
        IVolunteerService VolunteerService { get; }
        IAuthenticationService AuthenticationService { get; }
    }
}

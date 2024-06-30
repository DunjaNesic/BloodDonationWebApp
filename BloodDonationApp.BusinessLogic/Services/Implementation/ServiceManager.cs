using BloodDonationApp.BusinessLogic.Services.Contracts;
using BloodDonationApp.DataAccessLayer.UnitOfWork;
using BloodDonationApp.DataTransferObject.Donors;
using BloodDonationApp.DataTransferObject.Mappers;
using BloodDonationApp.Domain.DomainModel;
using BloodDonationApp.LoggerService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.BusinessLogic.Services.Implementation
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IActionService> _actionService;
        private readonly Lazy<IDonorService> _donorService;
        private readonly Lazy<IPlaceService> _placeService;
        private readonly Lazy<IQuestionnaireService> _questionnaireService;
        private readonly Lazy<IQuestionService> _questionService;
        private readonly Lazy<IVolunteerService> _volunteerService;

        public ServiceManager(IUnitOfWork uow, ILoggerManager _logger)
        {
            _actionService = new Lazy<IActionService>(() => new ActionService(uow, _logger));
            _donorService = new Lazy<IDonorService>(() => new DonorService(uow, _logger));
            _placeService = new Lazy<IPlaceService>(() => new PlaceService(uow));
            _questionnaireService = new Lazy<IQuestionnaireService>(() => new QuestionnaireService(uow, _logger));
            _questionService = new Lazy<IQuestionService>(() => new QuestionService(uow));
            _volunteerService = new Lazy<IVolunteerService>(() => new VolunteerService(uow, _logger));
        }

        public IActionService ActionService => _actionService.Value;
        public IDonorService DonorService => _donorService.Value;
        public IPlaceService PlaceService => _placeService.Value;
        public IQuestionnaireService QuestionnaireService => _questionnaireService.Value;  
        public IQuestionService QuestionService => _questionService.Value;
        public IVolunteerService VolunteerService => _volunteerService.Value;
    }
}

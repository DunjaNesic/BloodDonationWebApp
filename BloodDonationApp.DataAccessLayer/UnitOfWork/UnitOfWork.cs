using BloodDonationApp.DataAccessLayer.ActionRepo;
using BloodDonationApp.DataAccessLayer.DonorCallsRepo;
using BloodDonationApp.DataAccessLayer.DonorRepo;
using BloodDonationApp.DataAccessLayer.PlaceRepo;
using BloodDonationApp.DataAccessLayer.QuestionnaireRepo;
using BloodDonationApp.DataAccessLayer.QuestionRepo;
using BloodDonationApp.DataAccessLayer.VolCallsRepo;
using BloodDonationApp.DataAccessLayer.VolunteerRepo;
using BloodDonationApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataAccessLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BloodDonationContext _context;
        private readonly Lazy<IActionRepository> _actionRepository;
        private readonly Lazy<IDonorRepository> _donorRepository;
        private readonly Lazy<IPlaceRepository> _placeRepository;
        private readonly Lazy<IQuestionnaireRepository> _questionnaireRepository;
        private readonly Lazy<IQuestionRepository> _questionRepository;
        private readonly Lazy<IVolunteerRepository> _volunteerRepository;
        private readonly Lazy<IDonorCallsRepository> _donorCallsRepository;
        private readonly Lazy<IVolCallsRepository> _volunteerCallsRepository;

        public UnitOfWork(BloodDonationContext context)
        {
            _context = context;
            _actionRepository = new Lazy<IActionRepository>(() => new ActionRepository(_context));
            _donorRepository = new Lazy<IDonorRepository>(() => new DonorRepository(_context));
            _placeRepository = new Lazy<IPlaceRepository>(() => new PlaceRepository(_context));
            _questionnaireRepository = new Lazy<IQuestionnaireRepository>(() => new QuestionnaireRepository(_context));
            _questionRepository = new Lazy<IQuestionRepository>(() => new QuestionRepository(_context));
            _volunteerRepository = new Lazy<IVolunteerRepository>(() => new VolunteerRepository(_context));
            _donorCallsRepository = new Lazy<IDonorCallsRepository>(() => new DonorCallsRepository(_context));
            _volunteerCallsRepository = new Lazy<IVolCallsRepository>(() => new VolCallsRepository(_context));
        }

        public IActionRepository ActionRepository => _actionRepository.Value;
        public IDonorRepository DonorRepository => _donorRepository.Value;
        public IPlaceRepository PlaceRepository => _placeRepository.Value;
        public IQuestionnaireRepository QuestionnaireRepository => _questionnaireRepository.Value;
        public IQuestionRepository QuestionRepository => _questionRepository.Value;
        public IVolunteerRepository VolunteerRepository => _volunteerRepository.Value;
        public IDonorCallsRepository DonorCallsRepository => _donorCallsRepository.Value;
        public IVolCallsRepository VolunteerCallsRepository => _volunteerCallsRepository.Value;

        public async Task SaveChanges() => await _context.SaveChangesAsync();
    }
}

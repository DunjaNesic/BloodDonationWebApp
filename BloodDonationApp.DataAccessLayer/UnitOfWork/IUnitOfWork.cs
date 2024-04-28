using BloodDonationApp.DataAccessLayer.ActionRepo;
using BloodDonationApp.DataAccessLayer.DonorRepo;
using BloodDonationApp.DataAccessLayer.PlaceRepo;
using BloodDonationApp.DataAccessLayer.QuestionnaireRepo;
using BloodDonationApp.DataAccessLayer.QuestionRepo;
using BloodDonationApp.DataAccessLayer.VolunteerRepo;
using BloodDonationApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataAccessLayer.UnitOfWork
{
    public interface IUnitOfWork
    {
        IActionRepository ActionRepository { get; }
        IDonorRepository DonorRepository { get; }
        IPlaceRepository PlaceRepository { get; }
        IQuestionnaireRepository QuestionnaireRepository { get; }
        IQuestionRepository QuestionRepository { get; }
        IVolunteerRepository VolunteerRepository { get; }
        Task SaveChanges();
    }
}

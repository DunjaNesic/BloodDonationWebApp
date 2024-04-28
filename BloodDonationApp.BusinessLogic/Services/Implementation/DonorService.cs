using BloodDonationApp.BusinessLogic.Services.Contracts;
using BloodDonationApp.DataAccessLayer.UnitOfWork;
using BloodDonationApp.Domain.DomainModel;
using BloodDonationApp.Domain.ResponsesModel.BaseApiResponse;
using BloodDonationApp.Domain.ResponsesModel.ConcreteResponses;
using BloodDonationApp.Domain.ResponsesModel.ConcreteResponses.Donor;
using BloodDonationApp.Domain.ResponsesModel.Responses;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.BusinessLogic.Services.Implementation
{
    public class DonorService : IDonorService
    {
        private readonly IUnitOfWork uow;
        public DonorService(IUnitOfWork unitOfWork)
        {
            uow = unitOfWork;
        }

        public async Task<ApiBaseResponse> GetAll(bool trackChanges)
        {
            var donors = await uow.DonorRepository.GetAllAsync(trackChanges);
            return new ApiOkResponse<IQueryable<Donor>>(donors);
        }

        public async Task<ApiBaseResponse> GetByCondition(Expression<Func<Donor, bool>> condition, bool trackChanges)
        {
            var allDonors = await uow.DonorRepository.GetAllAsync(trackChanges);
            var foundDonor = allDonors.Where(condition);
            if (foundDonor.IsNullOrEmpty()) return new DonorNotFoundResponse();
            if (foundDonor.Count() > 1) return new DonorBadRequestResponse();
            return new ApiOkResponse<IQueryable<Donor>>(foundDonor);
        }
    }
}

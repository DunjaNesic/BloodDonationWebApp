using BloodDonationApp.DataTransferObject.Users;
using BloodDonationApp.Domain.DomainModel;
using BloodDonationApp.Domain.DTOs;
using BloodDonationApp.Domain.ResponsesModel.BaseApiResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.BusinessLogic.Services.Contracts
{
    public interface IAuthenticationService
    {
        Task<ApiBaseResponse> RegisterUser(UserRegistrationDTO userForRegistration);
        Task<int> ValidateUser(UserLoginDTO userForLogin);
        Task<TokenDTO> CreateToken(bool includeExpiry, int userID);
        Task<TokenDTO> RefreshToken(TokenDTO tokenDto);
        Task RevokeToken();
        Task<UserTypeDTO?> GetUserTypeAsync(int userId);
    }
}

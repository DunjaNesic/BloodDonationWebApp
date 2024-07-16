using BloodDonationApp.DataTransferObject.Users;
using BloodDonationApp.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.BusinessLogic.Services.Contracts
{
    public interface IAuthenticationService
    {
        Task RegisterUser(User userForRegistration);
        Task<bool> ValidateUser(UserLoginDTO userForLogin);
        Task<TokenDTO> CreateToken(bool includeExpiry);
        Task<TokenDTO> RefreshToken(TokenDTO tokenDto);
        Task RevokeToken();
    }
}

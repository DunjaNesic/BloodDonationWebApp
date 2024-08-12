using BloodDonationApp.BusinessLogic.Services.Contracts;
using BloodDonationApp.DataAccessLayer.BaseRepository;
using BloodDonationApp.DataAccessLayer.UnitOfWork;
using BloodDonationApp.DataTransferObject.Users;
using BloodDonationApp.Domain.DomainModel;
using BloodDonationApp.Domain.DTOs;
using BloodDonationApp.Domain.ResponsesModel.BaseApiResponse;
using BloodDonationApp.Domain.ResponsesModel.ConcreteResponses.Donor;
using BloodDonationApp.Domain.ResponsesModel.Responses;
using BloodDonationApp.LoggerService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BloodDonationApp.BusinessLogic.Services.Implementation
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _uow;
        private readonly ILoggerManager _logger;
        private readonly IConfiguration _configuration;
        public User? user { get; set; }
        public AuthenticationService(IUnitOfWork uow, ILoggerManager logger, IConfiguration configuration)
        {
            user = new User();
            _uow = uow;
            _logger = logger;
            _configuration = configuration;
        }
        public async Task<ApiBaseResponse> RegisterUser(UserRegistrationDTO registrationDTO)
        {
            var existingUser = await _uow.UserRepository.FindByEmailAsync(registrationDTO.Email);
            if (existingUser != null)
            {
                return new ApiOkResponse<string>("Email is already in use.");
            }

            var user = new User
            {
                Email = registrationDTO.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(registrationDTO.Password),
            };

            var role = await _uow.UserRepository.FindRoleAsync("donor-role");
            if (role != null)
            {
                user.Roles.Add(role);
            }
            else
            {
                return new ApiOkResponse<string>("Role does not exist.");
            }

            await _uow.UserRepository.CreateUser(user);
            await _uow.SaveChanges();

            var donor = new Donor
            {
                JMBG = registrationDTO.JMBG,
                DonorFullName = registrationDTO.DonorFullName,
                Sex = registrationDTO.Sex,
                BloodType = registrationDTO.BloodType,
                IsActive = registrationDTO.IsActive,
                PlaceID = registrationDTO.PlaceID,
                UserID = user.UserID,
                LastDonationDate = DateTime.UtcNow               
            };

            await _uow.DonorRepository.CreateDonor(donor);
            await _uow.SaveChanges(); 

            return new ApiOkResponse<string>("Registration successful");
        }

        public async Task<int> ValidateUser(UserLoginDTO userForLogin)
        {
            user = await _uow.UserRepository.FindByEmailAsync(userForLogin.Email);

            var result = (user != null && await _uow.UserRepository.CheckPasswordAsync(user, userForLogin.Password));
            if (!result)
            {
                _logger.LogWarning($"{nameof(ValidateUser)}: Authentication failed. Wrong user credentials");
                return 0;            
            }
            _logger.LogInformation($"{nameof(ValidateUser)}: Doing auth");
            return user?.UserID ?? 0;
        }

        public async Task<TokenDTO> CreateToken(bool includeExpiry, int userID)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims();
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            var refreshToken = GenerateRefreshToken();

            user.RefreshToken = refreshToken;

            if (includeExpiry)
                user.RefreshTokenExpiryTime = DateTime.Now.AddDays(3);

            _uow.UserRepository.Update(user);
            await _uow.SaveChanges();

            var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            TokenDTO token = new TokenDTO() {
            UserID = userID,
            AccessToken = accessToken,
            RefreshToken = refreshToken
            };

            return token;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("DUNJAsSECRET"));
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user?.Email)
            };

            var roles = await _uow.UserRepository.GetRolesAsync(user.UserID);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            var tokenOptions = new JwtSecurityToken
            (
                issuer: jwtSettings["validIssuer"],
                audience: jwtSettings["validAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
                signingCredentials: signingCredentials
            );

            return tokenOptions;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("DUNJAsSECRET"))),
                ValidateLifetime = true,
                ValidIssuer = jwtSettings["validIssuer"],
                ValidAudience = jwtSettings["validAudience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out
        securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null ||
        !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }

        public async Task<TokenDTO> RefreshToken(TokenDTO tokenDTO)
        {
            var principal = GetPrincipalFromExpiredToken(tokenDTO.AccessToken);

            User? targetUser = await _uow.UserRepository.FindByEmailAsync(principal?.Identity?.Name);
            if (targetUser == null || targetUser?.RefreshToken != tokenDTO.RefreshToken ||
            targetUser?.RefreshTokenExpiryTime <= DateTime.Now)
                throw new Exception("promenicu ovo posle");
            
            user = targetUser;
            return await CreateToken(false, user?.UserID ?? 0);
        }

        public Task RevokeToken()
        {
            throw new NotImplementedException();
        }

        public async Task<UserTypeDTO?> GetUserTypeAsync(int userId)
        {
            Expression<Func<Donor, bool>> donorCondition = donor => donor.UserID == userId;
            var donor = await _uow.DonorRepository.GetDonorsByCondition(donorCondition, false).FirstOrDefaultAsync();
            if (donor != null)
            {
                return new UserTypeDTO
                {
                    UserType = "Donor",
                    JMBG = donor.JMBG
                };
            }
            Expression<Func<Volunteer, bool>> volunteerCondition = vol => vol.UserID == userId;
            var volunteer = await _uow.VolunteerRepository.GetVolunteersByCondition(volunteerCondition, false).FirstOrDefaultAsync();
            if (volunteer != null)
            {
                return new UserTypeDTO
                {
                    UserType = "Volunteer",
                    VolunteerID = volunteer.VolunteerID
                };
            }

            return null;
        }
    }
 }

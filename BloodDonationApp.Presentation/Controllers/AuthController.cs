using BloodDonationApp.BusinessLogic.Services.Contracts;
using BloodDonationApp.DataTransferObject.Users;
using BloodDonationApp.Domain.DomainModel;
using BloodDonationApp.Presentation.ActionFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.Presentation.Controllers
{
    [ApiController]
    [Route("itk/auth")]
    public class AuthController : ApiBaseController
    {
        private readonly IServiceManager _serviceManager;
        public AuthController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO user)
        {
            if (user == null) return BadRequest("Invalid client request");

            if (!await _serviceManager.AuthenticationService.ValidateUser(user))
                return Unauthorized();

            var tokenDTO = await _serviceManager.AuthenticationService.CreateToken(true);

            return Ok(tokenDTO);
        }

        [HttpPost("/token")]
        public async Task<IActionResult> Refresh([FromBody] TokenDTO tokenDTO)
        {
            var tokenDtoToReturn = await _serviceManager.AuthenticationService.RefreshToken(tokenDTO);
            return Ok(tokenDtoToReturn);
        }

        [HttpPost]
        [Route("/revoke")]
        public async Task<IActionResult> Revoke()
        {
           await _serviceManager.AuthenticationService.RevokeToken();
            return Ok();
        }

        [HttpPost("/register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDTO registrationDTO)
        {
            if (registrationDTO == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid registration details.");
            }

            var result = await _serviceManager.AuthenticationService.RegisterUser(registrationDTO);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

    }
}

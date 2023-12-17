namespace Elearning.API.Controllers
{
    using Elearning.Contracts.Common;
    using Elearning.Contracts.Services;
    using Elearning.Entittes.Models;
    using Elearning.Services;
    using Elearning.Shared.DTOs;
    using Elearning.Shared.DTOs.IdentityUserDTOs;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using System.Net;
    using System.Net.Http.Headers;

    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;
        //private readonly IEmailService _emailService;
        public AuthenticateController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration, IAuthService authService/*,IEmailService emailService*/)
        {

            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _authService = authService;
            //_emailService = emailService;
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request");
                }

                var response = await _authService.Login(model);
                if (response.StatusCode == StatusCodes.Status409Conflict)
                {
                    return Conflict(response.Message);
                }
                //  return Ok(message);

                return Ok(new
                {
                    token = response.Data, //new JwtSecurityTokenHandler().WriteToken(token),
                                           // expiration = message.ValidTo
                });
                // }
                ////else
                ////{
                ////    return Unauthorized();
                ////}
            }
            catch (Exception ex)
            {
                //  _logger.Log(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("registeration")]
        // [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid payload");
                }

                var response = await _authService.Registeration(model);//UserRoles.User
                if (response.StatusCode == StatusCodes.Status400BadRequest)
                {
                    return BadRequest(response.Message);
                }

                return CreatedAtAction(nameof(Register), model);

            }
            catch (Exception ex)
            {
                //_logger.Log(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("users/{email}")]
        public async Task<IActionResult> GetUserProfile(string email)
        {
            var result = await _authService.GetUserProfile(email);
            return Ok(result);

        }


        [HttpPut()]
        public async Task<IActionResult> UpdateUserProfile(string email, [FromBody] UserProfileDto userProfileDto)
        {
            GenericResponseModel<object>? result = await _authService.UpdateUserProfile(email, userProfileDto);
            if (result != null && result.StatusCode == StatusCodes.Status200OK)
            {
                return Ok(result);
            }

            return BadRequest(result?.Message);
        }
        [HttpGet("VerifyOtp")]
        public async Task<IActionResult> VerifyOtp(string otp, string email)
        {
            try
            {
                var result = await _authService.VerifyOTP(otp, email);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest model)
        {
            try
            {
                return Ok(await _authService.ForgotPassword(model));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ForgotPasswordRequest model)
        {
            try
            {
                return Ok(await _authService.ChangePassword(model));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
        {
            try
            {
                var token = Request.Headers["Authorization"].ToString();
                model.Token = token.Substring("Bearer ".Length);

                return Ok(await _authService.ResetPassword(model));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

    }

}

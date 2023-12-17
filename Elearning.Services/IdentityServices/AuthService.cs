namespace Elearning.Repositiores.Identity_Repos
{

    using Elearning.Contracts.Common;
    using Elearning.Contracts.Repositories;
    using Elearning.Contracts.Services;
    using Elearning.Entittes.Models;
    using Elearning.Shared.DTOs;
    using Elearning.Shared.DTOs.IdentityUserDTOs;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;

    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IRepositoryWrapper _repository;
        private readonly IEmailService _emailService;

        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration,
             IRepositoryWrapper repositoryWrapper, IEmailService emailService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _repository = repositoryWrapper;
            _emailService = emailService;
        }
        public async Task<GenericResponseModel<object>> Registeration(RegisterModel model)
        {
            try
            {
                var userExists = await _userManager.FindByNameAsync(model.Username);
                if (userExists != null)
                {
                    return new GenericResponseModel<object> { Message = "User already exists" };

                }

                ApplicationUser user = new ApplicationUser()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.Username,

                };
                var createUserResult = await _userManager.CreateAsync(user, model.Password);
                if (!createUserResult.Succeeded)
                {
                    return new GenericResponseModel<object> { StatusCode = StatusCodes.Status417ExpectationFailed, Message = "User creation failed! Please check user details and try again." };

                }

                if (!await _roleManager.RoleExistsAsync(model.Role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(model.Role));
                }

                if (await _roleManager.RoleExistsAsync(model.Role))
                {
                    await _userManager.AddToRoleAsync(user, model.Role);
                }
                var savedUser = await _userManager.FindByEmailAsync(user.Email);
                if (savedUser == null)
                {
                    return new GenericResponseModel<object> { Message = "Not exist after save" };

                }
                var otp = GenerateOTP();

                // Save the OTP for verification later (e.g., in a database)
                var otpData = new OTPDataDTO
                {
                    Email = user.Email,
                    UserId = user.Id,
                    OTP = otp,
                    OTPExpiration = DateTime.Now.AddMinutes(3),
                };

                await _repository.OTPDataRepository.AddOTPData(otpData);
                var emailSubject = "Verification Email";
                var emailMessage = $"Your Code for verification  is: {otp}";
                await _emailService.SendEmailAsync(user.Email, emailSubject, emailMessage);
                return new GenericResponseModel<object> { Message = "User created successfully" };

            }
            catch (Exception ex)
            {
                throw new Exception("failed to register user in Registeration in authservice", ex);

            }
        }


        /// <summary>
        /// ////////////////
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<GenericResponseModel<object>> Login(LoginModel model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Username);
                if (user == null)
                {
                    return new GenericResponseModel<object> { StatusCode = 409, Message = "Invalid username or password" };
                }

                if (!await _userManager.CheckPasswordAsync(user, model.Password!))
                {
                    return new GenericResponseModel<object> { StatusCode = 409, Message = "Invalid username or password" };
                }


                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
            {
               new Claim(ClaimTypes.Name, user.UserName!),
               new Claim(ClaimTypes.Email, user.Email!),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

            };
                string rolesAsCSV = "";
                List<string> rolesList = new List<string>();
                foreach (var userRole in userRoles)
                {
                    rolesList.Add(userRole);
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                rolesAsCSV = string.Join(",", rolesList);
                authClaims.Add(new Claim("allRole", rolesAsCSV));

                string token = GenerateToken(authClaims);
                return new GenericResponseModel<object> { StatusCode = 200, Message = "user login successfully", Data = token };

            }
            catch (Exception ex)
            {
                throw new Exception("failed to login in AuthService", ex);
            }
        }
        public async Task<GenericResponseModel<object>> RegisterAdmin(RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
            {
                return new GenericResponseModel<object> { Message = "User creation failed! Please check user details and try again." };

            }
            //  return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Doctor already exists!" });

            ApplicationUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return new GenericResponseModel<object> { Message = "User creation failed! Please check user details and try again." };

            }
            // return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Doctor creation failed! Please check user details and try again." });

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            }

            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            }

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }

            return new GenericResponseModel<object> { Message = "User created successfully" };

        }
        private string GenerateToken(IEnumerable<Claim> claims)
        {
            try
            {
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"] ?? string.Empty));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = _configuration["JWT:ValidIssuer"],
                    Audience = _configuration["JWT:ValidAudience"],
                    Expires = DateTime.UtcNow.AddMinutes(2),
                    SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                    Subject = new ClaimsIdentity(claims)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new Exception("failed GenerateToken token in authservice", ex);
            }
        }

        public async Task<UserProfileDto?> GetUserProfile(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return null;
            }

            // Map user properties to a user profile DTO (Data Transfer Object)
            var userProfile = new UserProfileDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                // Include other properties as needed
            };

            return userProfile;
        }

        public async Task<GenericResponseModel<object>?> UpdateUserProfile(string email, UserProfileDto userProfileDto)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return null;
            }

            // Update user properties from the DTO
            user.UserName = userProfileDto.UserName;
            user.Email = userProfileDto.Email;
            user.PhoneNumber = userProfileDto.PhoneNumber;
            // Update other properties as needed

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new GenericResponseModel<object>() { Message = "user updated successfully" };
            }
            // step log log (result.Errors;)
            return new GenericResponseModel<object>() { Message = "failed to  updated user" };

        }


        public async Task<bool> VerifyOTP(string otp, string email)
        {
            var res = _repository.OTPDataRepository.FindByCondition(x => x.OTP == otp && x.Email == email &&
            DateTime.Now.Date <= x.OTPExpiration.Value.Date &&
            DateTime.Now.TimeOfDay <= x.OTPExpiration.Value.TimeOfDay).FirstOrDefault();
            if (res == null)
            {
                return false;
            }
            return true;
        }


        public async Task<object> ForgotPassword(ForgotPasswordRequest model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null || !await _userManager.IsEmailConfirmedAsync(user))
            {
                // User not found or email not confirmed
                // Note: You can customize the response based on your requirements
                return new GenericResponseModel<object>
                {
                    Data = "Not found",
                    StatusCode = StatusCodes.Status404NotFound
                };
                //return NotFound();
            }

            // Generate OTP
            var otp = GenerateOTP();

            // Save the OTP for verification later (e.g., in a database)
            var otpData = new OTPDataDTO
            {
                Email = user.Email,
                UserId = user.Id,
                OTP = otp,
                OTPExpiration = DateTime.Now.AddMinutes(2),
            };

            await _repository.OTPDataRepository.AddOTPData(otpData);
            var emailSubject = "Password Reset OTP";
            var emailMessage = $"Your OTP for password reset is: {otp}";
            await _emailService.SendEmailAsync(user.Email, emailSubject, emailMessage);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            // Return success response
            return new GenericResponseModel<object>
            {
                Data = token,
                StatusCode = StatusCodes.Status200OK
            };
            // return Ok();
        }


        public async Task<object> ChangePassword(ForgotPasswordRequest model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                // User not found
                return new GenericResponseModel<object>
                {
                    Data = "Not found",
                    StatusCode = StatusCodes.Status404NotFound
                };
                // return NotFound();
            }

            // Generate OTP
            var otp = GenerateOTP();
            var emailSubject = "Password Change OTP";
            var emailMessage = $"Your OTP for password change is: {otp}";
            await _emailService.SendEmailAsync(user.Email, emailSubject, emailMessage);

            // Store the OTP for verification later (e.g., in a database)

            // Return success response
            return new GenericResponseModel<object>
            {
                //  Data = ,
                StatusCode = StatusCodes.Status200OK
            };
            // return Ok();
        }

        public async Task<object> ResetPassword(ResetPasswordModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                // User not found
                return new GenericResponseModel<object>
                {
                    Data = "Not found",
                    StatusCode = StatusCodes.Status404NotFound
                };
                //  return NotFound();
            }

            // Verify the OTP

            // Reset the user's password
            var resetPasswordResult = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

            if (!resetPasswordResult.Succeeded)
            {
                // Password reset failed
                // Note: You can customize the response based on your requirements
                return new GenericResponseModel<object>
                {
                    Data = "Not found",
                    StatusCode = StatusCodes.Status400BadRequest
                };
                //  return BadRequest();
            }
            var emailContent = $"Your password is reset now";

            await _emailService.SendEmailAsync(user.Email, "Password Reset Complete", emailContent);

            //        return true;
            // Return successresponse
            return new GenericResponseModel<object>
            {

                StatusCode = StatusCodes.Status200OK
            };
            //  return Ok();
        }
        private string GenerateOTP()
        {
            // Generate a random OTP code (you can use any OTP generation algorithm)
            // For simplicity, let's assume a 6-digit numeric OTP
            var otp = new Random().Next(100000, 999999).ToString();

            return otp;
        }
    }
}



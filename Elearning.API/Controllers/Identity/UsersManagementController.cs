namespace Elearning.API.Controllers
{
    using Elearning.Contracts.Services;
    using Elearning.Entittes.Models;
    using Elearning.Services.Common;
    using Elearning.Shared.DTOs.IdentityUserDTOs;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    //[Authorize(Roles = "Admin")]
    [Route("api/[controller]/[action]")]
    public class UsersManagementController : BaseController
    {
       
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;
        public UsersManagementController( UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration, IAuthService authService)
        {
          
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _authService = authService;
        }
        
        [HttpGet]
        public IActionResult GetRoles()
        {
            try
            {
                var roles = _roleManager.Roles;
                return Ok(roles);
            }
            catch ( Exception )
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetRolesForUser(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if ( user == null )
                {
                    return BadRequest("invalid username");
                }
                // Get the roles for the user
                var roles = await _userManager.GetRolesAsync(user);
                return Ok(roles);
            }
            catch ( Exception ex )
            {
                //log 
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("registeration")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            try
            {
                if ( !ModelState.IsValid )
                {
                    return BadRequest("Invalid model");
                }

                var response = await _authService.Registeration(model);
                if (response.StatusCode ==  StatusCodes.Status400BadRequest)
                {
                    return BadRequest(response.Message);
                }

                return CreatedAtAction(nameof(Register), model);

            }
            catch ( Exception ex )
            {
                //_logger.Log(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRole creationRole)
        {

            if ( ModelState.IsValid )
            {
                IdentityRole identityRole = new IdentityRole()
                {
                    Name = creationRole.RoleName
                };
                IdentityResult identityResult = await _roleManager.CreateAsync(identityRole);
                if ( identityResult.Succeeded )
                {
                    return Ok(identityResult.Succeeded);
                }

                foreach ( IdentityError result in identityResult.Errors )
                {
                    //log
                    ModelState.AddModelError("", result.Description);
                }

                return BadRequest(identityResult.Errors);

            }

            return Ok(creationRole);
        }
        [HttpPut()]
        public async Task<IActionResult> EditUserRole(string email, [FromBody] EditUserRoleDto editUserRoleDto)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if ( user == null )
            {
                return NotFound();
            }

            var existingRoles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, existingRoles);
            if ( !result.Succeeded )
            {
                return BadRequest(result.Errors);
            }

            result = await _userManager.AddToRolesAsync(user, editUserRoleDto.Roles);
            if ( result.Succeeded )
            {
                return NoContent();
            }

            return BadRequest(result.Errors);
        }
        [HttpDelete()]
        public async Task<IActionResult> DeleteUserProfile(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if ( user == null )
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);
            if ( result.Succeeded )
            {
                return Ok("deleted successfully");
            }

            return BadRequest(result.Errors);
        }
    }
}
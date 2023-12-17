namespace Elearning.Services.Identity_Repos
{
    using Elearning.Contracts.Services;
    using Elearning.Entittes.Models;
    using Elearning.Shared.DTOs.IdentityUserDTOs;

    using Microsoft.AspNetCore.Identity;
    using Elearning.Contracts.Common;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public class UsersManagementService : IUsersManagementService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersManagementService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<object> GetRoles()
        {
            try
            {
                var roles = _roleManager.Roles;
                return roles;
            }
            catch ( Exception ex )
            {
                return ex.Message;
            }
        }
        public async Task<object> CreateRole(CreateRole creationRole)
        {

            IdentityRole identityRole = new IdentityRole()
            {
                Name = creationRole.RoleName
            };
            IdentityResult identityResult = await _roleManager.CreateAsync(identityRole);
            if ( identityResult.Succeeded )
            {
                return identityResult.Succeeded;
            }

            //foreach ( IdentityError result in identityResult.Errors )
            //{
            //    //log
            //    //  ModelState.AddModelError("", result.FnParameter);
            //}

            return identityResult.Errors;

            // }

        }

        public async Task<object> EditUserRole(string email, EditUserRoleDto editUserRoleDto)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if ( user == null )
            {
                return "NotFound";
            }

            var existingRoles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, existingRoles);
            if ( !result.Succeeded )
            {
                return result.Errors;
            }

            result = await _userManager.AddToRolesAsync(user, editUserRoleDto.Roles);
            if ( result.Succeeded )
            {
                return "Success";
            }

            return result.Errors;
        }

        public async Task<object> GetRolesForUser(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if ( user == null )
                {
                    return "invalid username";
                }
                // Get the roles for the user
                var roles = await _userManager.GetRolesAsync(user);
                return roles;
            }
            catch ( Exception ex )
            {
                //log 
                return ex.Message;
            }
        }

        public async Task<object> DeleteUserProfile(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if ( user == null )
            {
                return "Not found";// NotFound();
            }

            var result = await _userManager.DeleteAsync(user);
            if ( result.Succeeded )
            {
                return new GenericResponseModel<object>() {  Message = "user deleted successfully" };

            }

            return new GenericResponseModel<object>() {  Message = "failed to delete user " };
        }
    }
}

namespace Elearning.Contracts.Services
{
    using Elearning.Shared.DTOs.IdentityUserDTOs;

    public interface IUsersManagementService
    {
        Task<object> GetRoles();
        Task<object> CreateRole(CreateRole creationRole);
        Task<object> GetRolesForUser(string email);
        Task<object> EditUserRole(string email, EditUserRoleDto editUserRoleDto);
        Task<object> DeleteUserProfile(string email);
    }
}

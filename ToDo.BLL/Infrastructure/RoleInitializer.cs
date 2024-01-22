using Microsoft.AspNetCore.Identity;
using ToDo.BLL.Interfaces;
using ToDo.DAL.Entities.Identity;

namespace ToDo.BLL.Infrastructure
{
    public class RoleInitializer : IRoleInitializer
    {
        private readonly UserManager<UserDto> _userManager;
        private readonly RoleManager<RoleDto> _roleManager;

        public RoleInitializer(UserManager<UserDto> userManager, RoleManager<RoleDto> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void InitializeIdentityData()
        {
            RegisterRoleAsync(CustomRoles.UserRole).Wait();
        }

        private async Task<RoleDto> RegisterRoleAsync(string roleName)
        {

            var role = await _roleManager.FindByNameAsync(roleName);

            if (role != null)
            {
                return role;
            }

            role = new RoleDto(roleName);
            await _roleManager.CreateAsync(role);

            return role;
        }
    }
}

using Microsoft.AspNetCore.Identity;

namespace ToDo.DAL.Entities.Identity;

public class UserRoleDto : IdentityUserRole<string>
{
    public virtual UserDto UserDto { get; set; }

    public virtual RoleDto RoleDto { get; set; } 
}


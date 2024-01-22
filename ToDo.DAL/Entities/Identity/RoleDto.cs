using Microsoft.AspNetCore.Identity;

namespace ToDo.DAL.Entities.Identity;

public class RoleDto : IdentityRole
{
    public RoleDto() : base() { } 

    public RoleDto(string roleName) : base(roleName) { }

    public virtual List<UserRoleDto> UserRoles { get; set; }
}
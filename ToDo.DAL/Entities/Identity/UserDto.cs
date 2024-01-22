using Microsoft.AspNetCore.Identity;

namespace ToDo.DAL.Entities.Identity;

public class UserDto : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public virtual List<UserRoleDto> UserRoles { get; set; } = new List<UserRoleDto>();
    public virtual List<TaskDto>? Tasks { get; set; } = new List<TaskDto>();
}
using ToDo.DAL.Entities.Abstract;
using ToDo.DAL.Entities.Identity;

namespace ToDo.DAL.Entities
{
    public class TaskDto : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public virtual UserDto UserDto { get; set; } = new UserDto();
    }
}

namespace ToDo.BLL.Models.Request
{
    public class TaskRequest
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
    }
}

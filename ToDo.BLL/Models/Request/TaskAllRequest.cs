namespace ToDo.BLL.Models.Request
{
    public class TaskAllRequest
    {
        public string? SearchString { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}

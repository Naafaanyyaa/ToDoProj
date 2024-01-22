namespace ToDo.BLL.Models.Response
{
    public class TaskResponse : BaseResponse  
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}

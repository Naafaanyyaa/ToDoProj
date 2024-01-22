using ToDo.BLL.Models.Request;
using ToDo.BLL.Models.Response;

namespace ToDo.BLL.Interfaces
{
    public interface ITaskService
    {
        Task<List<TaskResponse>> GetAllTasksByRequest(TaskAllRequest request, string userId);
        Task<List<TaskResponse>> GetAllTasks(string userId);
        Task<TaskResponse> CreateAsync(TaskRequest request, string userId);
        Task DeleteByIdAsync(string userId, string taskId);
        Task<TaskResponse> UpdateByIdAsync(string userId, string taskId, TaskRequest request);
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using Microsoft.IdentityModel.Tokens;
using ToDo.BLL.Infrastructure.Expressions;
using ToDo.BLL.Interfaces;
using ToDo.BLL.Models.Request;
using ToDo.BLL.Models.Response;
using ToDo.DAL.Entities;
using ToDo.DAL.Entities.Identity;
using ToDo.DAL.Interfaces;
using ToDo.BLL.Exceptions;

namespace ToDo.BLL.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskDtoRepository _taskDtoRepository;
        private readonly UserManager<UserDto> _userManager;
        private readonly ILogger<TaskService> _logger;
        private readonly IMapper _mapper;

        public TaskService(ITaskDtoRepository taskDtoRepository, UserManager<UserDto> userManager, ILogger<TaskService> logger, IMapper mapper)
        {
            _taskDtoRepository = taskDtoRepository;
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<TaskResponse>> GetAllTasksByRequest(TaskAllRequest request, string userId)
        {

            var predicate = CreateFilterPredicate(request, userId);
            var source = await _taskDtoRepository.GetAsync(predicate, includes: new List<Expression<Func<TaskDto, object>>>()
            {
                x => x.UserId
            });

            var result = _mapper.Map<List<TaskDto>, List<TaskResponse>>(source);
            return result;
        }

        public async Task<List<TaskResponse>> GetAllTasks(string userId)
        {
            var owner = await _userManager.FindByIdAsync(userId);

            if (owner == null)
            {
                throw new NotFoundException("User is not found");
            }

            var taskList = await _taskDtoRepository.GetAsync(task => task.UserId == userId);

            var result = _mapper.Map<List<TaskDto>, List<TaskResponse>>(taskList);

            return result;
        }

        public async Task<TaskResponse> CreateAsync(TaskRequest request, string userId)
        {
            var owner = await _userManager.FindByIdAsync(userId);

            if (owner == null)
            {
                throw new NotFoundException("User is not found");
            }

            var taskDto = _mapper.Map<TaskRequest, TaskDto>(request);

            taskDto.CreatedDate = DateTime.Now;

            var insertResponse = await _taskDtoRepository.AddAsync(taskDto);

            _logger.LogInformation("Task {TaskId} has been successfully registered", taskDto.Id);

            var result = _mapper.Map<TaskDto, TaskResponse>(insertResponse);

            return result;
        }

        public async Task DeleteByIdAsync(string userId, string taskId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new NotFoundException("User is not found");
            }

            var task = await _taskDtoRepository.GetByIdAsync(taskId);

            if (task == null)
            {
                throw new NotFoundException("Task is not found");
            }

            if (!task.UserId.Equals(user.Id))
            {
                throw new ValidationException("Task was not created by this user");
            }

            task.IsDeleted = true;

            await _taskDtoRepository.UpdateAsync(task);
        }

        public async Task<TaskResponse> UpdateByIdAsync(string userId, string taskId, TaskRequest request)
        {
            var task = await _taskDtoRepository.GetByIdAsync(taskId);

            if (task == null)
            {
                throw new ValidationException("Task is not found");
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new NotFoundException("User is not found");
            }

            if (task.UserId != user.Id)
            {
                throw new ValidationException("Task was not created by this user");
            }

            var updatedHospitalInfo = _mapper.Map<TaskRequest, TaskDto>(request);

            updatedHospitalInfo.Id = task.Id;
            updatedHospitalInfo.CreatedDate = task.CreatedDate;
            updatedHospitalInfo.LastModifiedDate = DateTime.Now;

            await _taskDtoRepository.UpdateAsync(updatedHospitalInfo);

            var result = _mapper.Map<TaskDto, TaskResponse>(updatedHospitalInfo);

            return result;
        }

        private Expression<Func<TaskDto, bool>>? CreateFilterPredicate(TaskAllRequest request, string userId)
        {
            Expression<Func<TaskDto, bool>>? predicate = null;

            if (!string.IsNullOrWhiteSpace(request.SearchString))
            {
                Expression<Func<TaskDto, bool>> searchStringExpression = x =>
                    (!x.Title.IsNullOrEmpty() && x.Title.Contains(request.SearchString)) ||
                    (!x.Description.IsNullOrEmpty() && x.Description.Contains(request.SearchString));
                
                predicate = ExpressionsHelper.And(predicate, searchStringExpression);
            }

            if (request.StartDate.HasValue && request.EndDate.HasValue && request.StartDate < request.EndDate)
            {
                Expression<Func<TaskDto, bool>> dateExpression = x => x.CreatedDate > request.StartDate.Value
                                                                      && x.CreatedDate < request.EndDate.Value;
                predicate = ExpressionsHelper.And(predicate, dateExpression);
            }

            if (!userId.IsNullOrEmpty())
            {
                Expression<Func<TaskDto, bool>> userExpression = x => x.UserId.Equals(userId);

                predicate = ExpressionsHelper.And(predicate, userExpression);
            }

            return predicate;
        }
    }
}

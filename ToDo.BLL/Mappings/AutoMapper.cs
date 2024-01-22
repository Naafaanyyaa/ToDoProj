using AutoMapper;
using ToDo.BLL.Models.Request;
using ToDo.BLL.Models.Response;
using ToDo.DAL.Entities;
using ToDo.DAL.Entities.Identity;

namespace ToDo.BLL.Mappings
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<RegistrationRequest, UserDto>()
                .MaxDepth(1);
            CreateMap<UserDto, RegistrationResponse>();
            CreateMap<TaskRequest, TaskDto>();
            CreateMap<TaskDto, TaskResponse>();
        }
    }
}
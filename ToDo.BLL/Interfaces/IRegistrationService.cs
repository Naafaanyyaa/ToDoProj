using ToDo.BLL.Models.Request;
using ToDo.BLL.Models.Response;

namespace ToDo.BLL.Interfaces
{
    public interface IRegistrationService
    {
        Task<RegistrationResponse> Registration(RegistrationRequest request);
    }
}
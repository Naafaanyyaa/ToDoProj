using System.Security.Claims;
using ToDo.BLL.Models.Request;
using ToDo.BLL.Models.Response;

namespace ToDo.BLL.Interfaces
{
    public interface ILoginService
    {
        Task<List<Claim>> Login(LoginRequest request);
    }
}

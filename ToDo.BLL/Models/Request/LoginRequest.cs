using System.ComponentModel.DataAnnotations;

namespace ToDo.BLL.Models.Request
{
    public class LoginRequest
    {
        [Required]
        public string UserName { set; get; } = string.Empty;
        [Required]
        public string Password { set; get; } = string.Empty;
    }
}

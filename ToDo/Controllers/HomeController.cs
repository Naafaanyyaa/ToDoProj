using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ToDo.BLL.Interfaces;
using ToDo.Models;

namespace ToDo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITaskService _taskService;
        public HomeController(ILogger<HomeController> logger, ITaskService taskService)
        {
            _logger = logger;
            _taskService = taskService;
        }

        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction(nameof(UserController.Unauthorized), "User");

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Chamados54WebApp.Areas.Admin.Controllers
{    
    [Authorize(Roles = "Administrador,Tecnico,Cliente")]
    public class HomeController : AdminController
    {
        public HomeController(IWebHostEnvironment webHostEnvironment) : base(webHostEnvironment)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

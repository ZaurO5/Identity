using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    [Authorize(Roles = "Directior, HR")]
    public class VacancyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    public class AboutController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}

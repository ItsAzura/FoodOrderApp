using Microsoft.AspNetCore.Mvc;

namespace FoodOrderApp.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

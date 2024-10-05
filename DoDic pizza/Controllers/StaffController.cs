using Microsoft.AspNetCore.Mvc;

namespace DoDic_pizza.Controllers
{
    public class StaffController : Controller
    {
        public IActionResult Index()
        {
            ViewData["ImageUrl1"] = "/images/Лёха.jpg";
            return View();
        }
    }
}

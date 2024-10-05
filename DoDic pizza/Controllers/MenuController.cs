using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace DoDic_pizza.Controllers
{
    public class MenuController : Controller
    {
        public ActionResult Main()
        {
            ViewData["ImageUrl1"] = "/images/пицца1.jpg";
            ViewData["ImageUrl2"] = "/images/пицца2.jpg";
            ViewData["ImageUrl3"] = "/images/пицца3.jpg";
            ViewData["ImageUrl4"] = "/images/пицца4.jpg";
            ViewData["ImageUrl5"] = "/images/пицца5.jpg";
            ViewData["DrinkImage1"] = "/images/фанки манки.jpg";
            ViewData["DrinkImage2"] = "/images/фанки манки.jpg";
            ViewData["DrinkImage3"] = "/images/фанки манки.jpg";
            return View();
        }

        public ActionResult Pizza()
        {
            ViewData["ImageUrl1"] = "/images/пицца1.jpg";
            ViewData["ImageUrl2"] = "/images/пицца2.jpg";
            ViewData["ImageUrl3"] = "/images/пицца3.jpg";
            ViewData["ImageUrl4"] = "/images/пицца4.jpg";
            ViewData["ImageUrl5"] = "/images/пицца5.jpg";
            return View();
        }

        public ActionResult Drinks()
        {
            ViewData["DrinkImage1"] = "/images/фанки манки.jpg";
            ViewData["DrinkImage2"] = "/images/фанки манки.jpg";
            ViewData["DrinkImage3"] = "/images/фанки манки.jpg";
            return View();
        }

        public ActionResult Combo()
        {
            ViewData["ComboImage1"] = "/images/Комбо 1.jpg";
            return View();
        }
    }
}

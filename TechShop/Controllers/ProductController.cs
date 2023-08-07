using Microsoft.AspNetCore.Mvc;

namespace TechShop.Controllers
{
	public class ProductController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
        public IActionResult Detail()
        {
            return View();
        }
    }
}

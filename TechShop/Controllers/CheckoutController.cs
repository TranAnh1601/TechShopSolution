using Microsoft.AspNetCore.Mvc;

namespace TechShop.Controllers
{
	public class CheckoutController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}

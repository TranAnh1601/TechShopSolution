using Microsoft.AspNetCore.Mvc;
using Practice.Models;
using System.Diagnostics;

namespace Practice.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private List<Student> _students;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _students = new List<Student>()
            {
                new Student(1, "messis", 15),
                new Student(2, "ronaldo", 16),
                new Student(3, "foden", 19),
            };    
        }

        public IActionResult Index()
        {
            return View(_students);
        }

        public IActionResult Privacy()
        {
            return Json(_students);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
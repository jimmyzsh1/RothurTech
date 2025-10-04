using System.Diagnostics;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Models;

namespace MovieShopMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Title = "MovieShop Home Page Title";
            //ViewData["Title"] = "MovieShop Home Page Title";

            var movies = new List<MovieCard>
            {
                new MovieCard { Title = "Inception", Id = 1, PosterUrl = "" },
                new MovieCard { Title = "Interstellar", Id = 2, PosterUrl = "" },
                new MovieCard { Title = "The Dark Knight", Id = 3, PosterUrl = ""},
                new MovieCard { Title = "Deadpool", Id = 4, PosterUrl = ""},
                new MovieCard { Title = "The Avengers", Id = 5, PosterUrl = ""}
            };
            return View(movies);
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

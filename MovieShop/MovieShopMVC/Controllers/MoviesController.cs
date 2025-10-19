using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        public async Task<IActionResult> Details(int id) //http://movieshop.com/movies/details/1  // int 改成 Task<int>, void 改成 Task
        {
            var movieDetails = await _movieService.GetMovieDetails(id);

            return View(movieDetails);
        }

        public async Task<IActionResult> Genres(int id, int pageSize = 30, int pageNumber = 1)
        {
            var pagedMovies = await _movieService.GetMoviesByGenrePagination(id, pageSize, pageNumber);
            return View(pagedMovies);
        }
    }
}

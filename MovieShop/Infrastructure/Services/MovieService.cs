using ApplicationCore.Contracts.Services; // 引入接口命名空间
using ApplicationCore.Models; // 引入 MovieCard 所在命名空间
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Repositories;

namespace Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public List<MovieCard> Get30HighestGrossingMovies()
        {
            var movies = _movieRepository.Get30HighestGrossingMovies();
            var movieCards = new List<MovieCard>();

            foreach (var movie in movieCards)
            {
                movieCards.Add(new MovieCard { Id = movie.Id, Title = movie.Title, PosterUrl = movie.PosterUrl });
            };
            return movieCards;
        }

    }
}

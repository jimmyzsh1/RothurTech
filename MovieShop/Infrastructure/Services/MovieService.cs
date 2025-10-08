using ApplicationCore.Contracts.Services; // 引入接口命名空间
using ApplicationCore.Models; // 引入 MovieCard 所在命名空间
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        public List<MovieCard> GetTop30GrossingMovies()
        {
            var movies = new List<MovieCard>
            {
                new MovieCard { Title = "Inception", Id = 1, PosterUrl = "" },
                new MovieCard { Title = "Interstellar", Id = 2, PosterUrl = "" },
                new MovieCard { Title = "The Dark Knight", Id = 3, PosterUrl = ""},
                new MovieCard { Title = "Deadpool", Id = 4, PosterUrl = ""},
                new MovieCard { Title = "The Avengers", Id = 5, PosterUrl = ""}
            };
            return movies;
        }

    }
}

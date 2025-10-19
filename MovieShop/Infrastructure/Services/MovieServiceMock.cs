using ApplicationCore.Contracts.Services; // 引入接口命名空间
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class MovieServiceMock //: IMovieService
    {


        public List<MovieCard> Get30HighestGrossingMovies()
        {
            var movies = new List<MovieCard>
            {
                new MovieCard { Title = "Inception", Id = 11, PosterUrl = "" },
                new MovieCard { Title = "Interstellar", Id = 22, PosterUrl = "" },
                new MovieCard { Title = "The Dark Knight", Id = 33, PosterUrl = ""},
                new MovieCard { Title = "Deadpool", Id = 44, PosterUrl = ""},
                new MovieCard { Title = "The Avengers", Id = 55, PosterUrl = ""}
            };
            return movies;

        }

        public MovieDetailModel GetMovieDetails(int id)
        {
            throw new NotImplementedException();
        }
    }
}

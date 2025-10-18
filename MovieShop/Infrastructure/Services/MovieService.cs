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

            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCard { Id = movie.Id, Title = movie.Title, PosterUrl = movie.PosterUrl });
            };
            return movieCards;
        }

        public MovieDetailModel GetMovieDetails(int id)
        {
            var movie = _movieRepository.GetById(id);
            var movieDetails = new MovieDetailModel
            {
                id = movie.Id,
                Title = movie.Title,
                Overview = movie.Overview,
                Tagline = movie.Tagline,
                Budget = movie.Budget,
                Revenue = movie.Revenue,
                ImdbUrl = movie.ImdbUrl,
                TmdbUrl = movie.TmdbUrl,
                PosterUrl = movie.PosterUrl,
                BackdropUrl = movie.BackdropUrl,
                OriginalLanguage = movie.OriginalLanguage,
                ReleaseDate = movie.ReleaseDate,
                RunTime = movie.RunTime,
                Price = movie.Price,
                Rating = movie.Rating,
            };

            // calculate average rating
            if (movie.Reviews != null && movie.Reviews.Count > 0)
            {
                movieDetails.AverageRating = Math.Round(movie.Reviews.Average(r => r.Rating), 1);
            }
            else
            {
                movieDetails.AverageRating = null;
            }

            movieDetails.Trailers = new List<TrailerModel>();
            foreach (var trailer in movie.Trailers)
            {
                movieDetails.Trailers.Add(new TrailerModel { Id = trailer.Id, Name = trailer.Name, TrailerUrl = trailer.TrailerUrl });
            }

            movieDetails.Genres = new List<GenreModel>();
            foreach (var genre in movie.GenresOfMovie)
            {
                movieDetails.Genres.Add(new GenreModel { Id = genre.GenreId, Name = genre.Genre.Name });
            }


            // Add Cast as homework requires
            movieDetails.Casts = new List<CastModel>();
            foreach(var mc in movie.MovieCasts)
            {
                movieDetails.Casts.Add(new CastModel
                {
                    Id = mc.CastId,
                    Name = mc.Cast.Name,
                    ProfilePath = mc.Cast.ProfilePath,
                    Character = mc.Character
                });
            }

            return movieDetails;

        }
    }
}

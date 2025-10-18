﻿using System;
using ApplicationCore.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Services
{
    public interface IMovieService
    {
        List<MovieCard> Get30HighestGrossingMovies();
        MovieDetailModel GetMovieDetails(int id);
    }
}

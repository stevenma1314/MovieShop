using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MovieShop.Entities;
using MovieShop.Data;

namespace MovieShop.Services
{
    public class MovieService : IMovieService
    {
        IMovieRepository _movierepository;
        public MovieService(IMovieRepository movierepository)
        {
            _movierepository = movierepository;
        }
        public async Task<IEnumerable<Movie>> GetHighestMovie()
        {
            return await _movierepository.GetHighestMovie();
        }
    }
}

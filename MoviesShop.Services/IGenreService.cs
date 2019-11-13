using MovieShop.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace MoviesShop.Services
{
    public interface IGenreService
    {
        Task<IEnumerable<Genre>> GetAllGenres();
        Task<IEnumerable<Movie>> GetMoviebyGenre(int Id);
    }
}

using MoviesShop.Services;
using System;
using System.Collections.Generic;
using System.Text;
using MovieShop.Data;
using System.Threading.Tasks;
using MovieShop.Entities;
using System.Linq;

namespace MovieShop.Services
{
    
    public class GenreService: IGenreService
    {
        private readonly IGenreRepositorycs _genreRepositorycs;
        public GenreService(IGenreRepositorycs genreRepositorycs)
        {
            _genreRepositorycs = genreRepositorycs;
        }

        public async Task<IEnumerable<Genre>> GetAllGenres()
        {
            var genres = await _genreRepositorycs.GetAllAsync();
            return genres.OrderBy(g => g.Name);
        }

        public Task<IEnumerable<Movie>> GetMoviebyGenre(int genrenID)
        {
            var movie = _genreRepositorycs.GetMovieByGenre(genrenID);
            return movie;
        }

     
    }
}

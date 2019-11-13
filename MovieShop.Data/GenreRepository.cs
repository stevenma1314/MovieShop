using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MovieShop.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MovieShop.Data
{
    public class GenreRepository : Repository<Genre>,IGenreRepositorycs
    {
        public GenreRepository(MoviesShopDbcontext dbcontext): base(dbcontext)
        {

        }

        public async Task<IEnumerable<Movie>> GetMovieByGenre(int GenreId)
        {
            var movies = await _dbContext.MovieGenre.Where(g => g.GenreId == GenreId).Include(m => m.Movie).ToListAsync();

            return movies.Select(m => m.Movie).ToList();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MovieShop.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MovieShop.Data
{
    public class MovieRepository: Repository<Movie>, IMovieRepository
    {
        public MovieRepository(MoviesShopDbcontext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Movie>> GetHighestMovie()
        {
            return await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(25).ToListAsync();

        }
    }
}

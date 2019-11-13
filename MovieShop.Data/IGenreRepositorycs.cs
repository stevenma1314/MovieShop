using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MovieShop.Entities;

namespace MovieShop.Data
{
   public  interface IGenreRepositorycs: IRepository<Genre>
    {
        Task<IEnumerable<Movie>> GetMovieByGenre(int Genre);
    }
}

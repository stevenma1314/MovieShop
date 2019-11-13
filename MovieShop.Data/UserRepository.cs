using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MovieShop.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MovieShop.Data
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(MoviesShopDbcontext dbcontext) :base (dbcontext)
        {

        }

       

        public async Task<User> GetUserByEmail(string emial)
        {
           
            var isNull = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == emial);
            return isNull;
        }

        public async Task<IEnumerable<Purchase>> GetUserPurchasedMovie(int userId)
        {
            var userMovies = await _dbContext.Purchases.Where(p => p.UserId == userId).Include(p =>p.Movie).ToListAsync();
            return userMovies;
        }
    }
}

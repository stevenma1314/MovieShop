using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MovieShop.Entities;

namespace MovieShop.Data
{
    public interface IUserRepository : IRepository<User>
    {
       Task<User>GetUserByEmail(string emial);

       Task<IEnumerable<Purchase>> GetUserPurchasedMovie(int UserId);

        

    }
}

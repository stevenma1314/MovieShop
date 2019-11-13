using System;
using System.Collections.Generic;
using System.Text;

namespace MovieShop.Services
{
    public interface ICryptoService
    {
      string HashPassword(string password,string salt);
      string CreateSalt();
    }
}

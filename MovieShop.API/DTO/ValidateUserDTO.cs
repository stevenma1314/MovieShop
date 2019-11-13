using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.API.DTO
{
    public class ValidateUserDTO
    {
        public string email { get; set; }
        public string password { get; set; }
    }
}

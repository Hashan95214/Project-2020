using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cakeworld.Models;

namespace cakeworld.Services.JWT_Services
{
    public interface IJWTService
    {
        public string GenerateJWTtoken(Login user);
    }
}


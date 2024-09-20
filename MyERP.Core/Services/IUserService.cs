using MyERP.Core.DTOs;
using MyERP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyERP.Core.Services
{
    public interface IUserService : IGenericService<User>
    {
        User GetByEmail(string email);
        Task<Token> Login(UserLoginDto userLoginDto);
    }
}

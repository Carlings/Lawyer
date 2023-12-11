using Common;
using LawyerDataBase.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Lawyer.BLL.Interfaces
{
    public interface IAccountService
    {
        Result<User> Register(User user);
        ClaimsIdentity Authenticate(User user);
        Result<ClaimsIdentity> Login(string login, string password);

    }
}

using Common;
using Lawyer.BLL.Helpers;
using Lawyer.BLL.Interfaces;
using LawyerDataBase.DAL.Entities;
using LawyerDataBase.DAL.Interfaces;
using System.Security.Claims;

namespace Lawyer.BLL.Services
{
    public class AccountService : IAccountService
    {
        IUserRepository userRepository;
        IUserRoleRepository _userRoleRepository;
        public AccountService(IUserRepository userRepository, IUserRoleRepository userRoleRepository)
        {
            this.userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
        }

        public ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, _userRoleRepository.GetAll().First(x => x.Id == user.UserRoleID).Name),
                new Claim("UserID", user.Id.ToString())
            };
            return new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }

        public Result<ClaimsIdentity> Login(string login, string password)
        {
            var user = userRepository.GetAll().FirstOrDefault(x => x.Login == login);

            if (user == null)
            {
                return new Result<ClaimsIdentity>
                {
                    Success = false,
                    Message = "Користувача з вказаним логіном не існує"
                };
            }

            if(HashPasswordHelper.HashPassword(password) != user.Password)
            {
                return new Result<ClaimsIdentity>
                {
                    Success = false,
                    Message = "Невірний пароль"
                };
            }

            var claims = Authenticate(user);

            return new Result<ClaimsIdentity>
            {
                Success = true,
                Data = claims
            };

        }

        public Result<User> Register(User user)
        {
            try
            {
                user.Password = HashPasswordHelper.HashPassword(user.Password);

                var match = userRepository.GetAll().FirstOrDefault(u => u.Name == user.Name);
                match = userRepository.GetAll().FirstOrDefault(u => u.Login == user.Login);

                if (match == null)
                {
                    userRepository.AddUser(user);
                    return new Result<User> { Success = true };
                }
                else
                    return new Result<User> { Success = false, Message = "Такий користувач вже існує" };
            }
            catch (Exception ex)
            {
                return new Result<User> { Success = false, Message = ex.Message };
                throw;
            }
        }

        
    }
}

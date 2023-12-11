using Common;
using LawyerDataBase.DAL.Entities;
using LawyerDataBase.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LawyerDataBase.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private LawyerDataBaseContext _lawyerData;

        public UserRepository(LawyerDataBaseContext dbContext)
        {
            _lawyerData = dbContext;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _lawyerData.Users.ToListAsync();
        } 

        public async Task<User> Find(int id)
        {
            return await _lawyerData.Users.Where(user => user.Id == id).SingleAsync<User>();
        }

        public void AddUser(User user)
        {
            _lawyerData.Users.Add(user);
            _lawyerData.SaveChanges();
        }

        public IQueryable<User> GetAll()
        {
            return _lawyerData.Users;
        }

        public Result<User> Delete(int id)
        {
            var user = _lawyerData.Users.Where(user => user.Id == id).FirstOrDefault();

            if (user != null)
            {
                _lawyerData.Users.Remove(user);
                _lawyerData.SaveChanges();

                return new Result<User>
                {
                    Success = true,
                    Data = user
                };
            }
            else
                return new Result<User>
                {
                    Success = false,
                    Data = user
                };
        }

        public Result<User> Update(User user)
        {
            try
            {
                _lawyerData.Update(user);
                _lawyerData.SaveChanges();

                return new Result<User>
                {
                    Success = true,
                    Data = user
                };
            }
            catch (Exception ex)
            {
                return new Result<User>
                {
                    Success = false,
                    Data = user,
                    Message = ex.Message
                };
            }
        }
    }
}

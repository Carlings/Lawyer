using Common;
using LawyerDataBase.DAL.Entities;

namespace LawyerDataBase.DAL.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        IQueryable<User> GetAll();
        Task<User> Find(int id);
        void AddUser(User user);
        Result<User> Delete(int id);
        Result<User> Update(User user);
        
    }
}

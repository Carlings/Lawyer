using LawyerDataBase.DAL.Entities;
using LawyerDataBase.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerDataBase.DAL.Repositories
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly LawyerDataBaseContext _db;
        public UserRoleRepository(LawyerDataBaseContext db)
        {
            _db = db;
        }
        public IQueryable<UserRole> GetAll()
        {
            return _db.UserRole;
        }
    }
}

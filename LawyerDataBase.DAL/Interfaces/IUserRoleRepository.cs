using LawyerDataBase.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LawyerDataBase.DAL.Interfaces
{
    public interface IUserRoleRepository
    {
        public IQueryable<UserRole> GetAll();
    }
}

using LawyerDataBase.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LawyerDataBase.DAL
{
    public class LawyerDataBaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<Document> Documents { get; set; }

        public LawyerDataBaseContext(DbContextOptions<LawyerDataBaseContext> contextOptions) : base(contextOptions)
        {
            Database.EnsureCreated();
        }
    }
}

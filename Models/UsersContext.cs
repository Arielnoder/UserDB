using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace usersdb.Models
{
    public class UsersContext : DbContext
    {
        
        public DbSet<UsersModel> Users {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder options) 
            =>options.UseSqlite(@"Data Source=users.db");
        
    }
}
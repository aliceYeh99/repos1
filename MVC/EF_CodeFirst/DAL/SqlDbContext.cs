using EF_CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EF_CodeFirst.DAL
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext() : base("name=CodeFirstDB2")
        {
            // <connectionStrings>  name=CodeFirstDB
            Database.SetInitializer<SqlDbContext>(new Initializer());
        }
        public DbSet<Person> People { get; set; }
    }
}
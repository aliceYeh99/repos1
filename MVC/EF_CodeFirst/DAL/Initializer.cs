using EF_CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EF_CodeFirst.DAL
{
    public class Initializer : DropCreateDatabaseIfModelChanges<SqlDbContext>
    {
        protected override void Seed(SqlDbContext context)
        {
            base.Seed(context);

            context.People.Add(
              new Person { Name = "Alice", BirthDate = new System.DateTime(1995, 2, 1) });
            context.People.Add(
              new Person { Name = "Bob", BirthDate = new System.DateTime(1995, 12, 5) });

            context.SaveChanges();

            Console.WriteLine("Database Initialized Done.");
        }
    }
}
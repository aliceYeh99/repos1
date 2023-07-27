using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EF_CodeFirst.Models
{
    public class GoodsContext : DbContext
    {
        //public DbSet<Car> Cars { get; set; }

        //public DbSet<CellPhone> CellPhones { get; set; }

        //public DbSet<Clothing> Clothing { get; set; }

        public GoodsContext()
            : base("GoodsContext") // connectionString's name
        {
        }
    }
}
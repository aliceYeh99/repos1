using EF_CodeFirst2.Models;
using EF_CodeFirst3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Web;


namespace EF_CodeFirst3.DAL
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext() : base("name=CodeFirstDB3")
        {
            // <connectionStrings>  name=CodeFirstDB
            //Database.SetInitializer<SqlDbContext>(new Initializer());
            //Database.SetInitializer<SqlDbContext>(null);
        }
        public DbSet<Person> People { get; set; }
        public DbSet<PMSToolGroup> PMSToolGroupList { get; set; }
        public DbSet<PMSOwner> PMSOwnerList { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Blog>()
        //        .HasMany(e => e.Posts)
        //        .WithOne()
        //        .HasForeignKey("BlogId")
        //        .IsRequired();
        //}

        // EF6 叫 DbModelBuilder, EF Core 才叫 ModelBuilder，而且 ef6 沒有 HasMany(e => e.Posts)
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // configures one-to-many relationship
            //modelBuilder.Entity<Student>()
            //    .HasRequired<Grade>(s => s.CurrentGrade)
            //    .WithMany(g => g.Students)
            //    .HasForeignKey<int>(s => s.CurrentGradeId);


            // 
            // fix PMSToolGroup 對到的 OwnerID 有重複值的問題
            // configures one-to-many relationship

            // 這個寫法成功了，但看不懂
            //modelBuilder.Entity<PMSToolGroup>()
            //    .HasRequired<PMSOwner>(s => s.OwnerObj)
            //    .WithMany(g => g.PMSToolGroups)
            //    .HasForeignKey<string>(s => s.OWNER);
           //new List<PMSToolGroup> { new PMSToolGroup() };
            modelBuilder.Entity<PMSToolGroup>()
    .HasRequired<PMSOwner>(s => s.OwnerObj)
    .WithMany(g => g.PMSToolGroups )
    .HasForeignKey<string>(s => s.OWNER)
    ;


//            modelBuilder.Entity<PMSToolGroup>()
//.HasRequired<PMSOwner>(s => s.OwnerObj)
//.WithMany(g => new List<PMSToolGroup> { g.PMSToolGroups.First<PMSToolGroup>() })
//.HasForeignKey<string>(s => s.OWNER);

        }
    }
}
using EFTestOneToMany.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFTestOneToMany.DAL
{
    internal class EntityModel
    {
        internal class EFTestModel : DbContext
        {
            public EFTestModel() : base("PeopleContext")
            {
            }
            public DbSet<People> People { get; set; }
            public DbSet<Owner> Owners { get; set; }
            public DbSet<PMSToolGroup> PMSToolGroupList { get; set; }
            public DbSet<OwnerDesc> OwnerDescList { get; set; }

            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                //modelBuilder.Entity<PMSToolGroup>()
                //    .HasMany<Owner>(e => e.Owners)
                //.WithMany(g => g.PMSToolGroups)
                    
                //    ;

                //// 這個寫法成功了，但看不懂
                ////modelBuilder.Entity<PMSToolGroup>()
                ////    .HasRequired<PMSOwner>(s => s.OwnerObj)
                ////    .WithMany(g => g.PMSToolGroups)
                ////    .HasForeignKey<string>(s => s.OWNER);
                ////new List<PMSToolGroup> { new PMSToolGroup() };
                //modelBuilder.Entity<PMSToolGroup>()
                //.HasRequired<Owner>(s => s.Owner)
                //.WithMany(g => g.PMSToolGroups)
                //.HasForeignKey<string>(s => s.OwnerId)
                //;

                ////modelBuilder.Entity<Owner>()
                ////.HasRequired<PMSToolGroup>(s => s.Owner)
                ////.WithMany(g => g.Owner)
                ////.HasForeignKey<string>(s => s.OwnerId)
                ////;

            }
        }
    }
}

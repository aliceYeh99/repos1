using EFTestOneToMany.DAL;
using EFTestOneToMany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EFTestOneToMany
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //PrintPeopleAddres();
            PrintToolGroups();
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
        private static void PrintToolGroups()
        {
            //Configure One to Many Relationship in Entity Framework Using Code First Approach  
            PMSToolGroup PMSToolG;

            using (var context = new EntityModel.EFTestModel())
            {
                Console.WriteLine("ToolGroup 列表");
                Console.WriteLine("--------------,   .MKF10, MKF10, FB02 有兩筆重複");
                int index = 1;
                //context.PMSToolGroupList.Distinct().Include("OwnerObj").ToList()
                //foreach (PMSToolGroup g in context.PMSToolGroupList.Include("Owner").ToList())
                foreach (PMSToolGroup g in context.PMSToolGroupList.Include("Owner").ToList().Distinct())
                {
                    //List<Owner> list = context.Owners.Include("Owner").ToList();
                    //context.PMSToolGroupList.Distinct().Include("OwnerObj").ToList()
                    PMSToolG = g;
                    
                    //Console.WriteLine("Owner Details");
                    //Console.WriteLine("Name:" + string.Join(" ", new object[]
                    //{
                    //    PMSToolG.Owner.OwnerId, PMSToolG.Owner.DESC
                    //}));

                    Console.WriteLine($"{index}." + string.Join(", ", new object[]
                    {
                            PMSToolG.OwnerId, PMSToolG.Owner.DESC, PMSToolG.PMSToolGroupId
                    }));

                    index++;
                }
            }
        }

        // 下面是 ok 的寫法
        //private static void xxPrintToolGroups()
        //{
        //    //Configure One to Many Relationship in Entity Framework Using Code First Approach  
        //    Owner Owner;

        //    using (var context = new EntityModel.EFTestModel())
        //    {

        //        {
        //            //List<Owner> list = context.Owners.Include("Owner").ToList();
        //            //context.PMSToolGroupList.Distinct().Include("OwnerObj").ToList()
        //            Owner = context.Owners.FirstOrDefault();
        //            int index = 1;
        //            Console.WriteLine("Owner Details");
        //            Console.WriteLine("Name:" + string.Join(" ", new object[]
        //            {
        //                Owner.OwnerId, Owner.OwnerId
        //            }));
        //            Console.WriteLine("ToolGroup 列表");
        //            Console.WriteLine("--------------");
        //            foreach (var PMSToolGroup in Owner.PMSToolGroups)
        //            {
        //                Console.WriteLine(index + string.Join(", ", new object[]
        //                {
        //                    PMSToolGroup.OwnerId, PMSToolGroup.OwnerId
        //            }));
        //                index += 1;
        //            }
        //        }
        //    }
        //}
        private static void PrintPeopleAddres()
        {
            //Configure One to Many Relationship in Entity Framework Using Code First Approach  
            People people;
            using (var context = new EntityModel.EFTestModel())
            {
                people = context.People.FirstOrDefault();
                int index = 1;
                Console.WriteLine("People Details");
                Console.WriteLine("Name:" + string.Join(" ", new object[]
                {
                    people.FirstName, people.LastName
                }));
                Console.WriteLine("Addresses");
                Console.WriteLine("---------");
                foreach (var address in people.PeopleAddress)
                {
                    Console.WriteLine(index + string.Join(", ", new object[]
                    {
                        address.AddressLine1, address.AddressLine2, address.City, address.State, address.Country
                }));
                    index += 1;
                }
            }
        }
    }
}

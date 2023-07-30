using EF_CodeFirst2.Models;
using EF_CodeFirst3.DAL;
using EF_CodeFirst3.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EF_CodeFirst3.Controllers
{
    public class HomeController : Controller
    {
        private SqlDbContext context = new SqlDbContext();
        public ActionResult Index()
        {
            // 如果把  [dbo].[__MigrationHistory] drop 掉， Model 根本起不來
            // 可以啦，只要把 初始化那行拿掉就好
            // Database.SetInitializer<SqlDbContext>(new Initializer()); --> 拿掉

            // System.NotSupportedException: 'Model compatibility cannot be checked because the database does not contain model metadata. Model compatibility can only be checked for databases created using Code First or Code First Migrations.'

            return View(context.People.ToList());
        }
        public ActionResult QueryPeople()
        {
            return View(context.People.ToList());
        }
        public ActionResult QueryToolG()
        {
            //return View(context.PMSToolGroupList.Include("OwnerObj").ToList());
            //Customer foundCustomer = context.Set<Customer>().Include(
            //// e => e.Orders.Select(d => d.OrderLines)).Find(custKey);\


            //var findEmployee = employees.Find(x => x.Salary > 10000);
            //return View(context.PMSToolGroupList.Include("OwnerObj").ToList().Distinct());
            return View(context.PMSToolGroupList.Distinct().Include("OwnerObj").ToList());
            //return View(context.PMSToolGroupList.Include("PMSOwners").ToList());
            //return View( new List<PMSToolGroup>(context.PMSToolGroupList)
            //    .FindAll( e =>  e.PMSOwners.Select( d => d.DESC)) ;
                
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
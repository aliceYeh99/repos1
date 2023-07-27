using EF_CodeFirst.DAL;
using EF_CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EF_CodeFirst.Controllers
{
    public class HomeController : Controller
    {
        private SqlDbContext context = new SqlDbContext();
        public ActionResult Index()
        {
            return View(context.People.ToList());
        }
        public ActionResult AddPerson()
        {
            context.People.Add(new Person { Name = "Alice" });
            context.SaveChanges();
            return RedirectToAction("Index");
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
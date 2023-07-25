﻿using DocumentFormat.OpenXml.Math;
using Steema.TeeChart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChartDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index2()
        {
            return View();
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

        public ActionResult GetChart()
        {
            int width = 800, height = 600;
            Steema.TeeChart.TChart mChart = new TChart();
            Steema.TeeChart.Styles.Bar mBar = new Steema.TeeChart.Styles.Bar();
            mChart.Header.Text = "TeeChart via ImageShap PNG example";
            mChart.Series.Add(mBar);
            mBar.FillSampleValues();
            mBar.XValues.DateTime = true; // mChart.Axes.Bottom.Lables.Angle = 90;
            mChart.Axes.Bottom.Increment = Steema.TeeChart.Utils.GetDateTimeStep(DateTimeSteps.OneDay);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            mChart.Export.Image.JPEG.Width = width;
            mChart.Export.Image.JPEG.Height = height;

            // TODO lock
            mChart.Export.Image.JPEG.Save(ms);
            ms.Position = 0;

            ms.Flush();
            System.Web.Mvc.FileContentResult res = File(ms.ToArray(), "Image/PNG", "");
            ms.Close();
            return res;


            
        }
    }
}
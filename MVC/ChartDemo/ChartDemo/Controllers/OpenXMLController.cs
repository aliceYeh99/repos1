using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Data;
using System.IO;
using System.Reflection;
using NPOI.SS.Formula.Functions;
using ChartDemo.Common;
using DocumentFormat.OpenXml.Packaging;

namespace ChartDemo.Controllers
{
    public class OpenXMLController : Controller
    {
        public ActionResult Export()
        {
            string filename = @"F:\tmp\title1.pptx";
            //SlideHelper.InsertNewSlide(@"C:\Users\Public\Documents\Myppt10.pptx", 1, "My new slide");
            using (MemoryStream stream = new MemoryStream())
            {
                //wb.SaveAs(stream);
                //wb.Write(stream);

                using (PresentationDocument presentationDocument = PresentationDocument.Open(filename, true))
                {
                    // Pass the source document and the position and title of the slide to be inserted to the next method.
                    //SlideHelper.InsertNewSlide(presentationDocument, position, slideTitle);
                    SlideHelper.InsertNewSlide(presentationDocument, 1, "My new slide");
                }

                return File(stream.ToArray(), "application/x-mspowerpoint", filename);
            }
        }

    }
}
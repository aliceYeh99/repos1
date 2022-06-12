using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using SvgExample;

namespace LinqSVGExample
{
    public class Program
    {
        //public static void Main(string[] args)
        //{
        //    CreateHostBuilder(args).Build().Run();
        //}

        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<Startup>();
        //        });
        // Template slide and sample image
        private const string EmptySlidePath = "Content\\EmptySlide.pptx";
        private const string SvgImagePath = "Content\\Dinosaur.svg";

        // Output paths
        private const string LinqToXmlOutputPath = "LinqToXmlSlide.pptx";
        private const string StronglyTypedOutputPath = "StronglyTypedSlide.pptx";

        // Image scale as a percentage of the slide height
        private const double PercentageOfCy = 0.5;

        public static void Main()
        {
            // Add an SVG image, using the Linq-to-XML way.
            File.Copy(EmptySlidePath, LinqToXmlOutputPath, true);
            using FileStream linqToXmlStream = File.Open(LinqToXmlOutputPath, FileMode.Open, FileAccess.ReadWrite);
            LinqToXmlTools.AddSvg(linqToXmlStream, SvgImagePath, PercentageOfCy);

            // Add an SVG image, using the strongly typed way.
            File.Copy(EmptySlidePath, StronglyTypedOutputPath, true);
            using FileStream stronglyTypedStream = File.Open(StronglyTypedOutputPath, FileMode.Open, FileAccess.ReadWrite);
            StronglyTypedTools.AddSvg(stronglyTypedStream, SvgImagePath, PercentageOfCy);
        }
    }
}

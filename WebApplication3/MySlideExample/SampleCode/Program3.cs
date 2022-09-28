using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;
using A = DocumentFormat.OpenXml.Drawing;
using System.IO;

namespace MySlideExample.SampleCode
{
    public class Program3
    {
        static int index = 1;
        static void Main(string[] args)
        {
            Console.WriteLine("Preparing Presentation");
            PopulateData();
            // GeneratedClass cls=new GeneratedClass();
            //cls.CreatePackage(@"E:\output.pptx");
            Console.WriteLine("Completed Presentation");
            Console.ReadLine();
        }

        public static void RunTest()
        {
            Console.WriteLine("Preparing Presentation");
            PopulateData();
            // GeneratedClass cls=new GeneratedClass();
            //cls.CreatePackage(@"E:\output.pptx");
            Console.WriteLine("Completed Presentation");
            Console.ReadLine();
        }

        private static void PopulateData()
        {
            var overflow = false;
            const int pageBorder = 3000000;
            var db = new AdventureWorksEntities();
            var products = db.Products;//.Take(5);
            //const string outputFile = @"E:\openxml\output.pptx";
            //File.Copy(@"E:\OpenXml\Template.pptx", outputFile, true);

            const string outputFile = @"F:\tmp\OpenXML\output_22_928.pptx";
            File.Copy(@"F:\tmp\OpenXML\EmptySlide.pptx", outputFile, true);

            using (var myPres = PresentationDocument.Open(outputFile, true))
            {
                var presPart = myPres.PresentationPart;
                var slideIdList = presPart.Presentation.SlideIdList;

                var list = slideIdList.ChildElements
                            .Cast<SlideId>()
                            .Select(x => presPart.GetPartById(x.RelationshipId))
                            .Cast<SlidePart>();


                var tableSlidePart = (SlidePart)list.Last();
                var current = tableSlidePart;
                long totalHeight = 0;
                foreach (var product in products)
                {

                    if (overflow)
                    {
                        var newTablePart = CloneSlidePart(presPart, tableSlidePart);
                        current = newTablePart;
                        overflow = false;
                        totalHeight = 0;
                    }




                    var tbl = current.Slide.Descendants<A.Table>().First();
                    var tr = new A.TableRow();
                    tr.Height = 200000;
                    tr.Append(CreateTextCell(product.Name));
                    tr.Append(CreateTextCell(product.ProductNumber));
                    tr.Append(CreateTextCell(product.Size));
                    tr.Append(CreateTextCell(String.Format("{0:00}", product.ListPrice)));
                    tr.Append(CreateTextCell(product.SellStartDate.ToShortDateString()));
                    tbl.Append(tr);


                    totalHeight += tr.Height;


                    if (totalHeight > pageBorder)
                        overflow = true;
                }
            }
        }


        static SlidePart CloneSlidePart(PresentationPart presentationPart, SlidePart slideTemplate)
        {
            //Create a new slide part in the presentation 
            SlidePart newSlidePart = presentationPart.AddNewPart<SlidePart>("newSlide" + index);
            index++;
            //Add the slide template content into the new slide 
            newSlidePart.FeedData(slideTemplate.GetStream(FileMode.Open));
            //make sure the new slide references the proper slide layout 
            newSlidePart.AddPart(slideTemplate.SlideLayoutPart);
            //Get the list of slide ids 
            SlideIdList slideIdList = presentationPart.Presentation.SlideIdList;
            //Figure out where to add the next slide (find max slide) 
            uint maxSlideId = 1;
            SlideId prevSlideId = null;
            foreach (SlideId slideId in slideIdList.ChildElements)
            {
                if (slideId.Id > maxSlideId)
                {
                    maxSlideId = slideId.Id;
                    prevSlideId = slideId;
                }
            }
            maxSlideId++;
            //Add new slide at the end of the deck 
            SlideId newSlideId = slideIdList.InsertAfter(new SlideId(), prevSlideId);
            //Make sure id and relid is set appropriately 
            newSlideId.Id = maxSlideId;
            newSlideId.RelationshipId = presentationPart.GetIdOfPart(newSlidePart);
            return newSlidePart;
        }


        private static A.TableCell CreateTextCell(string text)
        {
            var textCol = new string[2];
            if (!string.IsNullOrEmpty(text))
            {
                if (text.Length > 25)
                {
                    textCol[0] = text.Substring(0, 25);
                    textCol[1] = text.Substring(26);
                }
                else
                {
                    textCol[0] = text;
                }
            }
            else
            {
                textCol[0] = string.Empty;
            }


            A.TableCell tableCell3 = new A.TableCell();

            A.TextBody textBody3 = new A.TextBody();
            A.BodyProperties bodyProperties3 = new A.BodyProperties();
            A.ListStyle listStyle3 = new A.ListStyle();

            textBody3.Append(bodyProperties3);
            textBody3.Append(listStyle3);


            var nonNull = textCol.Where(t => !string.IsNullOrEmpty(t)).ToList();

            foreach (var textVal in nonNull)
            {
                //if (!string.IsNullOrEmpty(textVal))
                //{
                A.Paragraph paragraph3 = new A.Paragraph();
                A.Run run2 = new A.Run();
                //A.RunProperties runProperties2 = new A.RunProperties() { Language = "en-US", Dirty = false, SmartTagClean = false };
                A.RunProperties runProperties2 = new A.RunProperties() { Language = "en-US", Dirty = false };
                A.Text text2 = new A.Text();
                text2.Text = textVal;
                run2.Append(runProperties2);
                run2.Append(text2);
                paragraph3.Append(run2);
                textBody3.Append(paragraph3);
                //}
            }

            A.TableCellProperties tableCellProperties3 = new A.TableCellProperties();
            tableCell3.Append(textBody3);
            tableCell3.Append(tableCellProperties3);



            //var tc = new A.TableCell(
            //                    new A.TextBody(
            //                        new A.BodyProperties(),
            //                    new A.Paragraph(
            //                        new A.Run(
            //                            new A.Text(text)))),
            //                    new A.TableCellProperties());

            //return tc;
            return tableCell3;
        }
    }
}

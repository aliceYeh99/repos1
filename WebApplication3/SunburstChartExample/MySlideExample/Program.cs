using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Packaging;
using Drawing = DocumentFormat.OpenXml.Drawing;
using MySlideExample.SamleCode;

namespace MySlideExample
{
    public class Program
    {
        public static void Main1(string[] args)
        {
            //CreateHostBuilder(args).Build().Run(); //不知道要幹嘛
            CreatePowerPoint();

            AddCommentToPresentation(@"F:\tmp\OpenXML\Myppt1.pptx",
    "Katie Jordangggg", "KJ",
    "This is my programmatically added comment.");

            //新增一個 ppt 檔案
            string filepath = @"F:\tmp\OpenXML\PresentationFromFilename.pptx";
            Program1.CreatePresentation(filepath);


        }
        // Adds a comment to the first slide of the presentation document.
        // The presentation document must contain at least one slide.
        public static void AddCommentToPresentation(string file, string initials, string name, string text)
        {
            using (PresentationDocument doc = PresentationDocument.Open(file, true))
            {

                // Declare a CommentAuthorsPart object.
                CommentAuthorsPart authorsPart;

                // Verify that there is an existing comment authors part. 
                if (doc.PresentationPart.CommentAuthorsPart == null)
                {
                    // If not, add a new one.
                    authorsPart = doc.PresentationPart.AddNewPart<CommentAuthorsPart>();
                }
                else
                {
                    authorsPart = doc.PresentationPart.CommentAuthorsPart;
                }

                // Verify that there is a comment author list in the comment authors part.
                if (authorsPart.CommentAuthorList == null)
                {
                    // If not, add a new one.
                    authorsPart.CommentAuthorList = new CommentAuthorList();
                }

                // Declare a new author ID.
                uint authorId = 0;
                CommentAuthor author = null;

                // If there are existing child elements in the comment authors list...
                if (authorsPart.CommentAuthorList.HasChildren)
                {
                    // Verify that the author passed in is on the list.
                    var authors = authorsPart.CommentAuthorList.Elements<CommentAuthor>().Where(a => a.Name == name && a.Initials == initials);

                    // If so...
                    if (authors.Any())
                    {
                        // Assign the new comment author the existing author ID.
                        author = authors.First();
                        authorId = author.Id;
                    }

                    // If not...
                    if (author == null)
                    {
                        // Assign the author passed in a new ID                        
                        authorId = authorsPart.CommentAuthorList.Elements<CommentAuthor>().Select(a => a.Id.Value).Max();
                    }
                }

                // If there are no existing child elements in the comment authors list.
                if (author == null)
                {

                    authorId++;

                    // Add a new child element(comment author) to the comment author list.
                    author = authorsPart.CommentAuthorList.AppendChild<CommentAuthor>
                        (new CommentAuthor()
                        {
                            Id = authorId,
                            Name = name,
                            Initials = initials,
                            ColorIndex = 0
                        });
                }

                // Get the first slide, using the GetFirstSlide method.
                SlidePart slidePart1 = GetFirstSlide(doc);

                // Declare a comments part.
                SlideCommentsPart commentsPart;

                // Verify that there is a comments part in the first slide part.
                if (slidePart1.GetPartsOfType<SlideCommentsPart>().Count() == 0)
                {
                    // If not, add a new comments part.
                    commentsPart = slidePart1.AddNewPart<SlideCommentsPart>();
                }
                else
                {
                    // Else, use the first comments part in the slide part.
                    commentsPart = slidePart1.GetPartsOfType<SlideCommentsPart>().First();
                }

                // If the comment list does not exist.
                if (commentsPart.CommentList == null)
                {
                    // Add a new comments list.
                    commentsPart.CommentList = new CommentList();
                }

                // Get the new comment ID.
                uint commentIdx = author.LastIndex == null ? 1 : author.LastIndex + 1;
                author.LastIndex = commentIdx;

                // Add a new comment.
                Comment comment = commentsPart.CommentList.AppendChild<Comment>(
                    new Comment()
                    {
                        AuthorId = authorId,
                        Index = commentIdx,
                        DateTime = DateTime.Now
                    });

                // Add the position child node to the comment element.
                comment.Append(
                    new Position() { X = 100, Y = 200 },
                    new Text() { Text = text });

                // Save the comment authors part.
                authorsPart.CommentAuthorList.Save();

                // Save the comments part.
                commentsPart.CommentList.Save();
            }
        }
        // Get the slide part of the first slide in the presentation document.
        public static SlidePart GetFirstSlide(PresentationDocument presentationDocument)
        {
            // Get relationship ID of the first slide
            PresentationPart part = presentationDocument.PresentationPart;
            SlideId slideId = part.Presentation.SlideIdList.GetFirstChild<SlideId>();
            string relId = slideId.RelationshipId;

            // Get the slide part by the relationship ID.
            SlidePart slidePart = (SlidePart)part.GetPartById(relId);

            return slidePart;
        }
        private static void CreatePowerPoint()
        {
            // ref https://docs.microsoft.com/en-us/office/open-xml/how-to-insert-a-new-slide-into-a-presentation
            //string presentationFile = "C:/Users/AliceYeh/AppData/Local/Temp/myFirstPPT.pptx";
            string presentationFile = "F:/tmp/OpenXML/myFirstPPT.pptx";
            using (PresentationDocument presentationDocument = PresentationDocument.Open(presentationFile, true))
            {
                // Insert other code here.
                InsertNewSlide(presentationDocument,2, "Chart 圖表");
                InsertNewSlide(presentationDocument, 3, "Chartxx 圖表");
            }
        }

        // Insert the specified slide into the presentation at the specified position.
        public static void InsertNewSlide(PresentationDocument presentationDocument,
            int position, string slideTitle)
        {
            if (presentationDocument == null)
            {
                throw new ArgumentNullException("presentationDocument");
            }

            if (slideTitle == null)
            {
                throw new ArgumentNullException("slideTitle");
            }

            PresentationPart presentationPart = presentationDocument.PresentationPart;

            // Verify that the presentation is not empty.
            if (presentationPart == null)
            {
                throw new InvalidOperationException("The presentation document is empty.");
            }

            // Declare and instantiate a new slide.
            Slide slide = new Slide(new CommonSlideData(new ShapeTree()));
            uint drawingObjectId = 1;

            // Construct the slide content.            
            // Specify the non-visual properties of the new slide.
            NonVisualGroupShapeProperties nonVisualProperties = slide.CommonSlideData.ShapeTree.AppendChild(new NonVisualGroupShapeProperties());
            nonVisualProperties.NonVisualDrawingProperties = new NonVisualDrawingProperties() { Id = 1, Name = "" };
            nonVisualProperties.NonVisualGroupShapeDrawingProperties = new NonVisualGroupShapeDrawingProperties();
            nonVisualProperties.ApplicationNonVisualDrawingProperties = new ApplicationNonVisualDrawingProperties();

            //2.加簡報內容
            // Specify the group shape properties of the new slide.
            slide.CommonSlideData.ShapeTree.AppendChild(new GroupShapeProperties());


            // Declare and instantiate the title shape of the new slide.
            Shape titleShape = slide.CommonSlideData.ShapeTree.AppendChild(new Shape());

            drawingObjectId++;

            // Specify the required shape properties for the title shape. 
            titleShape.NonVisualShapeProperties = new NonVisualShapeProperties
                (new NonVisualDrawingProperties() { Id = drawingObjectId, Name = "Title" },
                new NonVisualShapeDrawingProperties(new Drawing.ShapeLocks() { NoGrouping = true }),
                new ApplicationNonVisualDrawingProperties(new PlaceholderShape() { Type = PlaceholderValues.Title }));
            titleShape.ShapeProperties = new ShapeProperties();

            // Specify the text of the title shape.
            titleShape.TextBody = new TextBody(new Drawing.BodyProperties(),
                    new Drawing.ListStyle(),
                    new Drawing.Paragraph(new Drawing.Run(new Drawing.Text() { Text = slideTitle })));

            //3.加 body 內文
            // Declare and instantiate the body shape of the new slide.
            Shape bodyShape = slide.CommonSlideData.ShapeTree.AppendChild(new Shape());
            drawingObjectId++;

            // Specify the required shape properties for the body shape.
            bodyShape.NonVisualShapeProperties = new NonVisualShapeProperties(
                    new NonVisualDrawingProperties() { Id = drawingObjectId, Name = "Content Placeholder" },
                    new NonVisualShapeDrawingProperties(new Drawing.ShapeLocks() { NoGrouping = true }),
                    new ApplicationNonVisualDrawingProperties(new PlaceholderShape() { Index = 1 }));
            bodyShape.ShapeProperties = new ShapeProperties();

            // Specify the text of the body shape.
            bodyShape.TextBody = new TextBody(new Drawing.BodyProperties(),
                    new Drawing.ListStyle(),
                    new Drawing.Paragraph());

            // 3最後一步最後部分創建一個新的幻燈片部分，找到指定的索引位置插入幻燈片，然後將其插入並保存修改後的演示文稿。


            // Create the slide part for the new slide.
            SlidePart slidePart = presentationPart.AddNewPart<SlidePart>();

            // Save the new slide part.
            slide.Save(slidePart);

            // Modify the slide ID list in the presentation part.
            // The slide ID list should not be null.
            SlideIdList slideIdList = presentationPart.Presentation.SlideIdList;

            // Find the highest slide ID in the current list.
            uint maxSlideId = 1;
            SlideId prevSlideId = null;

            foreach (SlideId slideId in slideIdList.ChildElements)
            {
                if (slideId.Id > maxSlideId)
                {
                    maxSlideId = slideId.Id;
                }

                position--;
                if (position == 0)
                {
                    prevSlideId = slideId;
                }

            }

            maxSlideId++;

            // Get the ID of the previous slide.
            SlidePart lastSlidePart;

            if (prevSlideId != null)
            {
                lastSlidePart = (SlidePart)presentationPart.GetPartById(prevSlideId.RelationshipId);
            }
            else
            {
                lastSlidePart = (SlidePart)presentationPart.GetPartById(((SlideId)(slideIdList.ChildElements[0])).RelationshipId);
            }

            // Use the same slide layout as that of the previous slide.
            if (null != lastSlidePart.SlideLayoutPart)
            {
                slidePart.AddPart(lastSlidePart.SlideLayoutPart);
            }

            // Insert the new slide into the slide list after the previous slide.
            SlideId newSlideId = slideIdList.InsertAfter(new SlideId(), prevSlideId);
            newSlideId.Id = maxSlideId;
            newSlideId.RelationshipId = presentationPart.GetIdOfPart(slidePart);

            // Save the modified presentation.
            presentationPart.Presentation.Save();
        
        }

        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            //using Stream pngStream = GeneralTools.ReadSvgAsPng(svgPath);
            //AddImagePart(slidePart, ImagePartType.Png, pngRelId, pngStream);
            Stream stream1 = GetStream("EmptySlide.pptx");
            MemoryStream memoryStream = new MemoryStream();
            int size1 = memoryStream.ToArray().Length;
            stream1.CopyTo(memoryStream);
            int size2 = memoryStream.ToArray().Length;
            byte[] byteData;
            using (var stream = memoryStream)
            using (var packageDocument = PresentationDocument.Open(memoryStream, true)) //stream.Path // pptx 寫到記憶體
            {
                //C:\Users\AliceYeh\AppData\Local\Temp
                // 在這裡 AddPresentationPart 會出錯
                //var presentationPart = presentationDoc.AddPresentationPart();
                //presentationPart.Presentation = new Presentation();
                AddImage(packageDocument, "f:/tmp/pie.jpg");

                byteData = stream.ToArray();
            }
            //CreatePresentation(stream_Path);

            File.WriteAllBytes("aaa.pptx", byteData);

            //AddImage(stream_Path, "f:/tmp/pie.jpg");
        }

        //直接寫檔
        public static void Main3(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            //using Stream pngStream = GeneralTools.ReadSvgAsPng(svgPath);
            //AddImagePart(slidePart, ImagePartType.Png, pngRelId, pngStream);
            Stream stream1 = GetStream("MySlideExample.Content.EmptySlide.pptx");
            MemoryStream stream2 = new MemoryStream();
            stream1.CopyTo(stream2);

            using (var stream = OpenFile("EmptySlide.pptx"))
            using (var packageDocument = PresentationDocument.CreateFromTemplate(stream_Path)) //stream.Path
            {
                //C:\Users\AliceYeh\AppData\Local\Temp
                // 在這裡 AddPresentationPart 會出錯
                //var presentationPart = presentationDoc.AddPresentationPart();
                //presentationPart.Presentation = new Presentation();
            }
            //CreatePresentation(stream_Path);

            AddImage(stream_Path, "f:/tmp/pie.jpg");
        }

        //public void Index(FormCollection form)
        //{
        //    try
        //    {
        //        //Save Image of jqPlot Chart to a file
        //        string fileName = Guid.NewGuid().ToString() + ".jpg";
        //        var data = form["imgData"].Replace("data:image/png;base64,", "");
        //        Byte[] bitmapData = new Byte[data.Length];
        //        bitmapData = Convert.FromBase64String(FixBase64ForImage(data));
        //        System.IO.MemoryStream streamBitmap = new System.IO.MemoryStream(bitmapData);
        //        //Bitmap bitImage = new Bitmap((Bitmap)Image.FromStream(streamBitmap));
        //        //bitImage.Save(Server.MapPath("~/content/") + fileName);// This statement will save file locally as an Image.

        //        //Insert the jqPlot Chart's Image into PowerPoint
        //        using (PresentationDocument prstDoc = PresentationDocument.Open(Server.MapPath("~/content/") + "Template.pptx", true))
        //        {
        //            string imgId = "rId" + new Random().Next(2000).ToString();
        //            ImagePart imagePart = prstDoc.PresentationPart.SlideParts.FirstOrDefault().AddImagePart(ImagePartType.Jpeg, imgId);
        //            imagePart.FeedData(new MemoryStream(bitmapData.ToArray()));
        //            DocumentFormat.OpenXml.Drawing.Blip blip = prstDoc.PresentationPart.SlideParts.FirstOrDefault().Slide.Descendants<documentformat.openxml.drawing.blip>().First();
        //            blip.Embed = imgId;
        //            prstDoc.PresentationPart.SlideParts.FirstOrDefault().Slide.Save();
        //            prstDoc.PresentationPart.Presentation.Save();
        //            prstDoc.Close();
        //        }
        //        return File(Server.MapPath("~/content/") + "Template.pptx", "application/mspowerpoint", "BarGraphPPT.pptx");
        //    }
        //    catch (Exception ex)
        //    {
        //        return Content(ex.Message);
        //    }
        //}
        public string FixBase64ForImage(string Image)
        {
            System.Text.StringBuilder sbText = new System.Text.StringBuilder(Image, Image.Length);

            sbText.Replace("\r\n", String.Empty);

            sbText.Replace(" ", String.Empty);

            return sbText.ToString();
        }


        //https://docs.microsoft.com/en-us/office/open-xml/how-to-insert-a-new-slide-into-a-presentation
        //https://stackoverflow.com/questions/35361079/how-i-add-image-in-powerpoint-with-openxml-c-sharp
        // 圖很小, google openxml imagepart size
        // Microsoft Sample Code 裡沒有的
        public static void AddImage(string file, string image)
        {
            using (var presentation = PresentationDocument.Open(file, true))
            {
                var slidePart = presentation
                    .PresentationPart
                    .SlideParts
                    .First();
                //var newPart = presentation
                //    .AddNewPart<SlidePart>(); //--> 不對
                //presentation.AddNewPart<SlidePart>(newPart); // 不對

                // keywork openxml ppt insert image imagepart size

                ImagePart part = slidePart
                    .AddImagePart(ImagePartType.Png);

                using (var stream = File.OpenRead(image))
                {
                    part.FeedData(stream);
                }
                
                var tree = slidePart
                    .Slide
                    .Descendants<DocumentFormat.OpenXml.Presentation.ShapeTree>()
                    .First();

                var picture = new DocumentFormat.OpenXml.Presentation.Picture();

                picture.NonVisualPictureProperties = new DocumentFormat.OpenXml.Presentation.NonVisualPictureProperties();
                picture.NonVisualPictureProperties.Append(new DocumentFormat.OpenXml.Presentation.NonVisualDrawingProperties
                {
                    Name = "My Shape",
                    Id = (UInt32)tree.ChildElements.Count - 1
                });

                var nonVisualPictureDrawingProperties = new DocumentFormat.OpenXml.Presentation.NonVisualPictureDrawingProperties();
                nonVisualPictureDrawingProperties.Append(new DocumentFormat.OpenXml.Drawing.PictureLocks()
                {
                    NoChangeAspect = true
                });
                picture.NonVisualPictureProperties.Append(nonVisualPictureDrawingProperties);
                picture.NonVisualPictureProperties.Append(new DocumentFormat.OpenXml.Presentation.ApplicationNonVisualDrawingProperties());

                var blipFill = new DocumentFormat.OpenXml.Presentation.BlipFill();
                var blip1 = new DocumentFormat.OpenXml.Drawing.Blip()
                {
                    Embed = slidePart.GetIdOfPart(part)
                };
                var blipExtensionList1 = new DocumentFormat.OpenXml.Drawing.BlipExtensionList();
                var blipExtension1 = new DocumentFormat.OpenXml.Drawing.BlipExtension()
                {
                    Uri = "{28A0092B-C50C-407E-A947-70E740481C1C}"
                };
                var useLocalDpi1 = new DocumentFormat.OpenXml.Office2010.Drawing.UseLocalDpi()
                {
                    Val = false
                };
                useLocalDpi1.AddNamespaceDeclaration("a14", "http://schemas.microsoft.com/office/drawing/2010/main");
                blipExtension1.Append(useLocalDpi1);
                blipExtensionList1.Append(blipExtension1);
                blip1.Append(blipExtensionList1);
                var stretch = new DocumentFormat.OpenXml.Drawing.Stretch();
                stretch.Append(new DocumentFormat.OpenXml.Drawing.FillRectangle());
                blipFill.Append(blip1);
                blipFill.Append(stretch);
                picture.Append(blipFill);

                picture.ShapeProperties = new DocumentFormat.OpenXml.Presentation.ShapeProperties();
                picture.ShapeProperties.Transform2D = new DocumentFormat.OpenXml.Drawing.Transform2D();
                picture.ShapeProperties.Transform2D.Append(new DocumentFormat.OpenXml.Drawing.Offset
                {
                    X = 0,
                    Y = 0,
                });
                picture.ShapeProperties.Transform2D.Append(new DocumentFormat.OpenXml.Drawing.Extents
                {
                    Cx = 10000 * 960,
                    Cy = 10000 * 640,
                });
                //960*640
                    //Cx = 1000000,
                    //Cy = 1000000,

                picture.ShapeProperties.Append(new DocumentFormat.OpenXml.Drawing.PresetGeometry
                {
                    Preset = DocumentFormat.OpenXml.Drawing.ShapeTypeValues.Rectangle
                });

                tree.Append(picture);
                
            }
        }

        // pptx 寫到記憶體
        public static void AddImage(PresentationDocument presentation1, string image)
        {
            using (var presentation = presentation1)
            {
                var slidePart = presentation
                    .PresentationPart
                    .SlideParts
                    .First();
                //var newPart = presentation
                //    .AddNewPart<SlidePart>(); //--> 不對
                //presentation.AddNewPart<SlidePart>(newPart); // 不對

                // keywork openxml ppt insert image imagepart size

                ImagePart part = slidePart
                    .AddImagePart(ImagePartType.Png);

                using (var stream = File.OpenRead(image))
                {
                    part.FeedData(stream);
                }

                var tree = slidePart
                    .Slide
                    .Descendants<DocumentFormat.OpenXml.Presentation.ShapeTree>()
                    .First();

                var picture = new DocumentFormat.OpenXml.Presentation.Picture();

                picture.NonVisualPictureProperties = new DocumentFormat.OpenXml.Presentation.NonVisualPictureProperties();
                picture.NonVisualPictureProperties.Append(new DocumentFormat.OpenXml.Presentation.NonVisualDrawingProperties
                {
                    Name = "My Shape",
                    Id = (UInt32)tree.ChildElements.Count - 1
                });

                var nonVisualPictureDrawingProperties = new DocumentFormat.OpenXml.Presentation.NonVisualPictureDrawingProperties();
                nonVisualPictureDrawingProperties.Append(new DocumentFormat.OpenXml.Drawing.PictureLocks()
                {
                    NoChangeAspect = true
                });
                picture.NonVisualPictureProperties.Append(nonVisualPictureDrawingProperties);
                picture.NonVisualPictureProperties.Append(new DocumentFormat.OpenXml.Presentation.ApplicationNonVisualDrawingProperties());

                var blipFill = new DocumentFormat.OpenXml.Presentation.BlipFill();
                var blip1 = new DocumentFormat.OpenXml.Drawing.Blip()
                {
                    Embed = slidePart.GetIdOfPart(part)
                };
                var blipExtensionList1 = new DocumentFormat.OpenXml.Drawing.BlipExtensionList();
                var blipExtension1 = new DocumentFormat.OpenXml.Drawing.BlipExtension()
                {
                    Uri = "{28A0092B-C50C-407E-A947-70E740481C1C}"
                };
                var useLocalDpi1 = new DocumentFormat.OpenXml.Office2010.Drawing.UseLocalDpi()
                {
                    Val = false
                };
                useLocalDpi1.AddNamespaceDeclaration("a14", "http://schemas.microsoft.com/office/drawing/2010/main");
                blipExtension1.Append(useLocalDpi1);
                blipExtensionList1.Append(blipExtension1);
                blip1.Append(blipExtensionList1);
                var stretch = new DocumentFormat.OpenXml.Drawing.Stretch();
                stretch.Append(new DocumentFormat.OpenXml.Drawing.FillRectangle());
                blipFill.Append(blip1);
                blipFill.Append(stretch);
                picture.Append(blipFill);

                picture.ShapeProperties = new DocumentFormat.OpenXml.Presentation.ShapeProperties();
                picture.ShapeProperties.Transform2D = new DocumentFormat.OpenXml.Drawing.Transform2D();
                picture.ShapeProperties.Transform2D.Append(new DocumentFormat.OpenXml.Drawing.Offset
                {
                    X = 0,
                    Y = 0,
                });
                picture.ShapeProperties.Transform2D.Append(new DocumentFormat.OpenXml.Drawing.Extents
                {
                    Cx = 10000 * 960,
                    Cy = 10000 * 640,
                });
                //960*640
                //Cx = 1000000,
                //Cy = 1000000,

                picture.ShapeProperties.Append(new DocumentFormat.OpenXml.Drawing.PresetGeometry
                {
                    Preset = DocumentFormat.OpenXml.Drawing.ShapeTypeValues.Rectangle
                });

                tree.Append(picture);

            }
        }





        #region 複製ppt


        /// <summary>
        /// Open a test file as an actual file on disk. Must be disposed to delete file
        /// </summary>
        /// <param name="name">Name of embedded resource</param>
        /// <param name="access">FileAccess type for file</param>
        /// <returns></returns>
        public static Stream OpenFile(string name)
        {
            return CopiedFile(GetStream(name), Path.GetExtension(name));
        }

        public static Stream GetStream(string name)
        {
            var assembly = typeof(Program).GetTypeInfo().Assembly;
            string na = $"MySlideExample.Content.{name}";
            var stream = assembly.GetManifestResourceStream($"MySlideExample.Content.{name}");
            var names = assembly.GetManifestResourceNames().OrderBy(t => t).ToList();

            //Assert.NotNull(stream);
            if (stream == null) throw new Exception("GetStream is null.");

            return stream;
        }
        static String stream_Path = "";


        public static Stream CopiedFile(Stream stream, string extension)
        {
            var Path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"{Guid.NewGuid()}{extension}");
            stream_Path = Path;
            //var _access = access;

            using (var fs = File.OpenWrite(Path))
            {
                stream.CopyTo(fs);
            }
            return stream;
        }

        #endregion
        public static PresentationDocument Clone(string path)
        {
            using (PresentationDocument template = PresentationDocument.Open(path, false))
            {
                // We've opened the template in read-only mode to let multiple processes or
                // threads open it without running into problems.
                PresentationDocument document = (PresentationDocument)template.Clone();

                // If the template is a document rather than a template, we are done.
                //if (extension == ".xlsx" || extension == ".xlsm")
                //{
                //    return document;
                //}

                // Otherwise, we'll have to do some more work.
                document.ChangeDocumentType(PresentationDocumentType.Presentation);

                // We are done, so save and return.
                // TODO: Check whether it would be safe to return without saving.
                document.Save();
                return document;
            }
        }

        public static void CreatePresentation(string filepath)
        {
            // Create a presentation at a specified file path. The presentation document type is pptx, by default.
            var presentationDoc = PresentationDocument.Create(filepath, PresentationDocumentType.Presentation);
            // 上面的已經做掉

            var presentationPart = presentationDoc.AddPresentationPart();
            presentationPart.Presentation = new Presentation();

            //todo fix 33
            //CreatePresentationParts(presentationPart);

            //presentationPart.GetXDocument();
            //        string sldRelId = presentationPart.GetXDocument()
            //.Elements(P.presentation)
            //.Elements(P.sldIdLst)
            //.Elements(P.sldId)
            //.Select(sldId => (string)sldId.Attribute(R.id)!)
            //.FirstOrDefault() ?? throw new InvalidOperationException(@"Presentation has no slides.");
            string sldRelId = "";//todo

            presentationPart.GetPartsOfType<OpenXmlPart>();
            //SlidePart slidePart1 = presentationPart.AddNewPart<SlidePart>();
            //foreach (OpenXmlPart P in presentationPart.GetPartsOfType<OpenXmlPart>())
            //{
            //    sldRelId = presentationPart.GetIdOfPart(P);

            //    // 取不到!! presentationPart
            //}




            SlidePart slidePart1 = presentationPart.AddNewPart<SlidePart>("rId2");
            GenerateSlidePart1Content(slidePart1);

            // Close the presentation handle
            presentationDoc.Close();
        }
        // Generates content of slidePart1.
        private static void GenerateSlidePart1Content(SlidePart slidePart1)
        {
            Slide slide1 = new Slide();
            slide1.AddNamespaceDeclaration("a", "http://schemas.openxmlformats.org/drawingml/2006/main");
            slide1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");
            slide1.AddNamespaceDeclaration("p", "http://schemas.openxmlformats.org/presentationml/2006/main");

            CommonSlideData commonSlideData2 = new CommonSlideData();

            ShapeTree shapeTree2 = new ShapeTree();

            NonVisualGroupShapeProperties nonVisualGroupShapeProperties2 = new NonVisualGroupShapeProperties();
            NonVisualDrawingProperties nonVisualDrawingProperties8 = new NonVisualDrawingProperties() { Id = 1U, Name = string.Empty };
            NonVisualGroupShapeDrawingProperties nonVisualGroupShapeDrawingProperties2 = new NonVisualGroupShapeDrawingProperties();
            ApplicationNonVisualDrawingProperties applicationNonVisualDrawingProperties8 = new ApplicationNonVisualDrawingProperties();

            nonVisualGroupShapeProperties2.Append(nonVisualDrawingProperties8);
            nonVisualGroupShapeProperties2.Append(nonVisualGroupShapeDrawingProperties2);
            nonVisualGroupShapeProperties2.Append(applicationNonVisualDrawingProperties8);

            GroupShapeProperties groupShapeProperties2 = new GroupShapeProperties();


        }

        public static void AddImagePart(SlidePart slidePart, ImagePartType imagePartType, string id, Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            ImagePart imagePart = slidePart.AddImagePart(imagePartType, id);
            imagePart.FeedData(stream);
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

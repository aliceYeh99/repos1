# repos1
MyFirstRepository

=================================================================================================================
=-筆記搜尋關鍵字 --> 用 open xml 寫圖片檔到 pptx
=================================================================================================================

https://osdn.net/projects/tortoisesvn/storage/1.14.3/Application/TortoiseSVN-1.14.3.29387-x64-svn-1.14.2.msi/

open xml 套件

https://github.com/OfficeDev/Open-XML-SDK
2.16.0

https://github.com/tkrotoff/PptxTemplater

https://github.com/nissl-lab/npoi

https://dotblogs.com.tw/shadow/2017/01/11/103354

2.Open XML SDK (請下載 v2.9.1) 
https://github.com/OfficeDev/Open-XML-SDK   v2.9.1

免費，但程式碼繁瑣，難以閱讀

https://stackoverflow.com/questions/35361079/how-i-add-image-in-powerpoint-with-openxml-c-sharp 

成功

https://pxm-software.com/openxml-power-point-templates-processing/



mvc 範例，剛好合用
public ActionResult Index(FormCollection form)
        {
            try
            {
                //Save Image of jqPlot Chart to a file
                string fileName = Guid.NewGuid().ToString() + ".jpg";
                var data = form["imgData"].Replace("data:image/png;base64,", "");
                Byte[] bitmapData = new Byte[data.Length];
                bitmapData = Convert.FromBase64String(FixBase64ForImage(data));
                System.IO.MemoryStream streamBitmap = new System.IO.MemoryStream(bitmapData);
                Bitmap bitImage = new Bitmap((Bitmap)Image.FromStream(streamBitmap));
                //bitImage.Save(Server.MapPath("~/content/") + fileName);// This statement will save file locally as an Image.

                //Insert the jqPlot Chart's Image into PowerPoint
                using (PresentationDocument prstDoc = PresentationDocument.Open(Server.MapPath("~/content/") + "Template.pptx", true))
                {
                    string imgId = "rId" + new Random().Next(2000).ToString();
                    ImagePart imagePart = prstDoc.PresentationPart.SlideParts.FirstOrDefault().AddImagePart(ImagePartType.Jpeg, imgId);
                    imagePart.FeedData(new MemoryStream(bitmapData.ToArray()));
                    DocumentFormat.OpenXml.Drawing.Blip blip = prstDoc.PresentationPart.SlideParts.FirstOrDefault().Slide.Descendants<documentformat.openxml.drawing.blip>().First();
                    blip.Embed = imgId;
                    prstDoc.PresentationPart.SlideParts.FirstOrDefault().Slide.Save();
                    prstDoc.PresentationPart.Presentation.Save();
                    prstDoc.Close();
                }
                return File(Server.MapPath("~/content/") + "Template.pptx", "application/mspowerpoint", "BarGraphPPT.pptx");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        public string FixBase64ForImage(string Image)
        {
            System.Text.StringBuilder sbText = new System.Text.StringBuilder(Image, Image.Length);

            sbText.Replace("\r\n", String.Empty);

            sbText.Replace(" ", String.Empty);

            return sbText.ToString();
        }

    }

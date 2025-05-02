# repos1
MyFirstRepository

Microsoft 參考文件
https://docs.microsoft.com/en-us/office/open-xml/how-to-insert-a-new-slide-into-a-presentation
======================================================
筆記搜尋關鍵字 --> 用 open xml 寫圖片檔到 pptx
======================================================

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



-----

當然可以！我已幫你**移除所有空白的中文名稱**，只保留有中英文配對的項目。以下是整理後、每筆用兩段 \`\`\` 分隔的內容：

---

```
Sinn. 'Pirate's Lost In Rainbow'
```

```
海盜的迷失在彩虹裡
```

```
Sinn. 'HCY's Fire Raindrops'
```

```
HCY的火雨滴
```

```
Sinn. 'WSL's Fervent'
```

```
WSL的熱情
```

```
Sinn. 'WSL's Might'
```

```
WSL的威武
```

```
Sinn. 'An's Nyx'
```

```
安的夜之女神
```

```
Sinn. 'Isa's Tropical Nectar'
```

```
伊莎的熱帶花蜜
```

```
Sinn. 'Isa's Flamboyant'
```

```
伊莎的龍飛鳳舞
```

```
Sinn. 'Fu's Mermaid'
```

```
芙的美人魚
```

```
Sinn. 'HCY's Lady Red'
```

```
HCY的淑女紅
```

```
Sinn. 'HCY's Sunlight'
```

```
HCY的日光
```

```
Sinn. 'Nora's True Love'
```

```
真愛
```

```
Sinn. 'Pirate's Lost In Rainbow'
```

```
海盜的迷失在彩虹裡
```

```
Sinn. 'An's Sparrow Pink'
```

```
安的麻雀粉
```

```
Sinn. 'Pirate's Daniel'
```

```
海盜的丹尼爾
```

```
Sinn. 'XY's White Mooning'
```

```
XY的白月光
```

```
Sinn. 'HCY's Peach Fragrance'
```

```
蜜桃香氛
```

---

如果你希望我幫你匯出成 `.txt` 或 `.md` 文件給你下載上傳，請告訴我，我可以立刻幫你產生。你還有其他花名也要處理嗎？

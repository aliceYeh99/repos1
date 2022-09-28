using System;
using System.Collections.Generic;

namespace MySlideExample.SampleCode
{
    internal class AdventureWorksEntities
    {
        public AdventureWorksEntities()
        {
            Products = new List<Product>();
            Products.Add(new Product("001","Pen", "50", 99.1));
            Products.Add(new Product("002","A4 Peper", "120", 150.5));
        }

        public  List<Product> Products { get; internal set; }
    }

    internal class Product
    {


        public Product(string v1, string v2, string v3, double v4)
        {
            this.ProductNumber = v1;
            this.Name = v2;
            this.Size = v3;
            this.ListPrice = v4;
            this.SellStartDate = DateTime.Now;
        }

        public string ProductNumber { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public double ListPrice { get; set; }
        public DateTime SellStartDate { get; set; }
    }
}
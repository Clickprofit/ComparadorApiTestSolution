using System;
namespace ForretasAPITester.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public string Title { get; set; }
        public string TitleSemAcentos { get; set; }
        public string Description { get; set; }
        public string Option { get; set; }
        public string Ean { get; set; }
        public string ImageUrl { get; set; }
        public string Referencia { get; set; }

        public Product()
        {
        }
    }
}

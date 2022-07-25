using System;
namespace ForretasAPITester.Models
{
    public class SearchProductsData
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
        public string Title { get; set; }
        public string Ean { get; set; }
        public int PriceMin { get; set; }
        public int PriceMax { get; set; }
        public bool OnlyActive { get; set; }

        public string APIResponse { get; set; }

        public SearchProductsData()
        {
        }
    }
}

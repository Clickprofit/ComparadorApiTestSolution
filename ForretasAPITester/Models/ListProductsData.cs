using System;
namespace ForretasAPITester.Models
{
    public class ListProductsData
    {
        public int Offset { get; set; }
        public int Limit { get; set; }

        public string APIResponse { get; set; }

        public ListProductsData()
        {
        }
    }
}

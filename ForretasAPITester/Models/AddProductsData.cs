using System;
using System.ComponentModel.DataAnnotations;

namespace ForretasAPITester.Models
{
    public class AddProductsData
    {
        [Required]
        public string DomainName { get; set; }

        [Required]
        public string Prefix { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter valid price (ex. 10.12)")]
        public double Price { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid value (ex. 10.12)")]
        public double PromotionPrice { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid value (ex. 10.12)")]
        public double PricePortas { get; set; }

        public bool NewEan { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "Please enter an integer 1-1000)")]
        public int NoOfProducts { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter an integer")]
        public int Offset { get; set; }

        public string ErrorMessage { get; set; }

        public string APIResponse { get; set; }

        public AddProductsData()
        {
        }
    }
}

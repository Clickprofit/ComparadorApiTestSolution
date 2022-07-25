using System;
using System.ComponentModel.DataAnnotations;

namespace ForretasAPITester.Models
{
    public class ProductInfoData
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid product Id")]
        public int ProductId { get; set; }

        public string APIResponse { get; set; }

        public ProductInfoData()
        {
        }
    }
}

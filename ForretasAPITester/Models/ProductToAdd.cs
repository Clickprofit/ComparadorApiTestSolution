using System;
using System.ComponentModel.DataAnnotations;

namespace ForretasAPITester.Models
{
    public class ProductToAdd
    {
        [Required]
        public string Ean { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Url { get; set; }
        public string Reference { get; set; }

        [Required]
        public decimal Price { get; set; }
        public decimal PromotionPrice { get; set; }
        public decimal ShippingPrice { get; set; }
        public decimal DeliveryPrice { get; set; }
        public decimal DeliveryPriceExpress { get; set; }
        public string Availability { get; set; }
        public string AvailabilityDate { get; set; }
        public int Stock { get; set; }
        public int MinDaysToPrepareShipping { get; set; }
        public int MaxDaysToPrepareShipping { get; set; }
        public int MinDeliveryDays { get; set; }
        public int MaxDeliveryDays { get; set; }
        public int MinDeliveryDaysExpress { get; set; }
        public int MaxDeliveryDaysExpress { get; set; }

        [Required]
        public string Image1 { get; set; }

        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public string Image4 { get; set; }
        public string Image5 { get; set; }

        public string Brand { get; set; }
        public string Category { get; set; }

        public ProductToAdd()
        {
        }
    }
}

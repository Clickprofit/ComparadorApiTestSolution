using System;
namespace ForretasAPITester.Models
{
    public class ProductMerchant
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime LastModified { get; set; }
        public decimal TotalPrice { get; set; }
        public int Currency { get; set; }
        public string Url { get; set; }
        public int ProductId { get; set; }
        public decimal ShippingPrice { get; set; }
        public bool Activo { get; set; }
        public int BrandId { get; set; }
        public int MerchantId { get; set; }
        public string Referencia { get; set; }
        public decimal Preco { get; set; }
        public decimal PrecoPromocao { get; set; }
        public decimal PrecoPortesEnvioNormal { get; set; }
        public decimal PrecoPortesEnvioExpresso { get; set; }
        public string Disponibilidade { get; set; }
        public int Stock { get; set; }
        public DateTime? Datadisponibelidade { get; set; }
        public int PrazoMinimoPreparacao { get; set; }
        public int PrazoMaximoPreparacao { get; set; }
        public int PrazoEntregaMinimoEnvioNormal { get; set; }
        public int PrazoEntregaMaximoEnvioNormal { get; set; }
        public int PrazoEntregaMinimoEnvioExpresso { get; set; }
        public int PrazoEntregaMaximoEnvioExpresso { get; set; }
        public string Ean { get; set; }

        public ProductMerchant()
        {
        }
    }
}

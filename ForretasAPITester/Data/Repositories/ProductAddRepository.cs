using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ForretasAPITester.Data.Interfaces;
using ForretasAPITester.Data.Repositories.Abstract;
using ForretasAPITester.Models;
using Microsoft.Extensions.Configuration;
using Dapper;
using ForretasAPITester.Helpers;
using ForretasAPITester.API;

namespace ForretasAPITester.Data.Repositories
{
    public class ProductAddRepository : AbstractRepository<ProductToAdd>, IProductAddRepository
    {
        public ProductAddRepository(IConfiguration configuration) : base(configuration) { }

        public List<ProductToAdd> GetProducts(AddProductsData data, int merchantId)
        {
            try
            {
                string query = $"SELECT Product.*, (SELECT TOP 1 NomeImagem FROM ProductImage " +
                    $"WHERE ProductId = Product.Id) AS 'ImageUrl' FROM Product WHERE Product.Id " +
                    $"NOT IN (SELECT ProductId FROM ProductMerchant WHERE MerchantId = {merchantId}) " +
                    $"ORDER BY Product.Id DESC OFFSET {data.Offset} " +
                    $"ROWS FETCH NEXT {data.NoOfProducts} ROWS ONLY";

                using (var conn = new SqlConnection(ConnectionString))
                {
                    var products = conn.QueryAsync<Product>(query).Result;

                    List<ProductToAdd> result = new List<ProductToAdd>();

                    foreach (var p in products)
                    {
                        ProductToAdd pa = new ProductToAdd
                        {
                            Ean = data.NewEan ? $"{data.Prefix}_{p.Ean}" : p.Ean,
                            Title = p.Title,
                            Description = p.Description,
                            Url = $"https://{data.DomainName}/{p.Title.MakeUrlSlug()}",
                            Reference = p.Referencia,
                            Price = (decimal)data.Price,
                            PromotionPrice = (decimal)data.PromotionPrice,
                            DeliveryPrice = (decimal)data.PricePortas,
                            Stock = 100,
                            Image1 = p.ImageUrl,
                            Brand = data.Brand,
                            Category = data.Category
                        };

                        result.Add(pa);
                    }

                    return result;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}

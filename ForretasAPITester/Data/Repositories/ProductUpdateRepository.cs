using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ForretasAPITester.Data.Interfaces;
using ForretasAPITester.Data.Repositories.Abstract;
using ForretasAPITester.Models;
using Microsoft.Extensions.Configuration;
using Dapper;
using ForretasAPITester.Helpers;

namespace ForretasAPITester.Data.Repositories
{
    public class ProductUpdateRepository : AbstractRepository<ProductToUpdate>, IProductUpdateRepository
    {
        public ProductUpdateRepository(IConfiguration configuration) : base(configuration) { }

        public List<ProductToUpdate> GetProducts(UpdateProductsData data, int merchantId)
        {
            try
            {
                string query = $"SELECT ProductMerchant.*, Product.Ean AS 'Ean' FROM ProductMerchant " +
                    $"INNER JOIN Product ON Product.Id = ProductMerchant.ProductId WHERE " +
                    $"ProductMerchant.MerchantId = {merchantId} " +
                    $"ORDER BY Product.Id DESC OFFSET {data.Offset} " +
                    $"ROWS FETCH NEXT {data.NoOfProducts} ROWS ONLY";

                using (var conn = new SqlConnection(ConnectionString))
                {
                    var merchantProducts = conn.QueryAsync<ProductMerchant>(query).Result;

                    List<ProductToUpdate> result = new List<ProductToUpdate>();

                    foreach (var mp in merchantProducts)
                    {
                        ProductToUpdate p = new ProductToUpdate
                        {
                            LastModified = DateTime.Now,
                            Ean = mp.Ean,
                            Title = mp.Title,
                            Description = mp.Description,
                            Url = mp.Url,
                            Reference = mp.Referencia,
                            Price = (decimal)data.Price,
                            TotalPrice = (decimal)data.Price,
                            PromotionPrice = (decimal)data.PromotionPrice,
                            ShippingPrice = mp.ShippingPrice,
                            DeliveryPrice = (decimal)data.PricePortas,
                            DeliveryPriceExpress = mp.PrecoPortesEnvioExpresso,
                            Availability = mp.Disponibilidade,
                            Stock = mp.Stock,
                            MinDaysToPrepareShipping = mp.PrazoMinimoPreparacao,
                            MaxDaysToPrepareShipping = mp.PrazoMaximoPreparacao,
                            MinDeliveryDays = mp.PrazoEntregaMinimoEnvioNormal,
                            MaxDeliveryDays = mp.PrazoEntregaMaximoEnvioNormal,
                            MinDeliveryDaysExpress = mp.PrazoEntregaMinimoEnvioExpresso,
                            MaxDeliveryDaysExpress = mp.PrazoEntregaMaximoEnvioExpresso
                        };

                        if (mp.Datadisponibelidade.HasValue)
                            p.AvailabilityDate = Convert.ToDateTime(mp.Datadisponibelidade).ToString();

                        result.Add(p);
                    }

                    return result;

                }
            }
            catch { return null; }
        }

        public List<ProductToUpdateById> GetProductsById(UpdateProductsData data, int merchantId)
        {
            try
            {
                string query = $"SELECT ProductMerchant.*, Product.Ean AS 'Ean' FROM ProductMerchant " +
                    $"INNER JOIN Product ON Product.Id = ProductMerchant.ProductId WHERE " +
                    $"ProductMerchant.MerchantId = {merchantId} " +
                    $"ORDER BY Product.Id DESC OFFSET {data.Offset} " +
                    $"ROWS FETCH NEXT {data.NoOfProducts} ROWS ONLY";

                using (var conn = new SqlConnection(ConnectionString))
                {
                    var merchantProducts = conn.QueryAsync<ProductMerchant>(query).Result;

                    List<ProductToUpdateById> result = new List<ProductToUpdateById>();

                    foreach (var mp in merchantProducts)
                    {
                        ProductToUpdateById p = new ProductToUpdateById
                        {
                            LastModified = DateTime.Now,
                            Id = mp.Id,
                            Title = mp.Title,
                            Description = mp.Description,
                            Url = mp.Url,
                            Reference = mp.Referencia,
                            Price = (decimal)data.Price,
                            TotalPrice = (decimal)data.Price,
                            PromotionPrice = (decimal)data.PromotionPrice,
                            ShippingPrice = mp.ShippingPrice,
                            DeliveryPrice = (decimal)data.PricePortas,
                            DeliveryPriceExpress = mp.PrecoPortesEnvioExpresso,
                            Availability = mp.Disponibilidade,
                            Stock = mp.Stock,
                            MinDaysToPrepareShipping = mp.PrazoMinimoPreparacao,
                            MaxDaysToPrepareShipping = mp.PrazoMaximoPreparacao,
                            MinDeliveryDays = mp.PrazoEntregaMinimoEnvioNormal,
                            MaxDeliveryDays = mp.PrazoEntregaMaximoEnvioNormal,
                            MinDeliveryDaysExpress = mp.PrazoEntregaMinimoEnvioExpresso,
                            MaxDeliveryDaysExpress = mp.PrazoEntregaMaximoEnvioExpresso
                        };

                        if (mp.Datadisponibelidade.HasValue)
                            p.AvailabilityDate = Convert.ToDateTime(mp.Datadisponibelidade).ToString();

                        result.Add(p);
                    }

                    return result;

                }
            }
            catch { return null; }
        }
    }
}

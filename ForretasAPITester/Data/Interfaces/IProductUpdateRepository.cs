using System;
using System.Collections.Generic;
using ForretasAPITester.Models;

namespace ForretasAPITester.Data.Interfaces
{
    public interface IProductUpdateRepository
    {
        public List<ProductToUpdate> GetProducts(UpdateProductsData data, int merchantId);
        public List<ProductToUpdateById> GetProductsById(UpdateProductsData data, int merchantId);
    }
}

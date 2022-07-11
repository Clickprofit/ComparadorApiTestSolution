using System;
using System.Collections.Generic;
using ForretasAPITester.Models;

namespace ForretasAPITester.Data.Interfaces
{
    public interface IProductAddRepository
    {
        public List<ProductToAdd> GetProducts(AddProductsData data, int merchantId);
    }
}

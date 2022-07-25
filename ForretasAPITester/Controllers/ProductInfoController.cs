using System;
using System.Linq;
using ForretasAPITester.API;
using ForretasAPITester.Models;
using Microsoft.AspNetCore.Mvc;

namespace ForretasAPITester.Controllers
{
    public class ProductInfoController : Controller
    {
        private readonly ForretasAPI api;

        public ProductInfoController(ForretasAPI forretasAPI)
        {
            api = forretasAPI;
        }

        public IActionResult Index()
        {
            ProductInfoData data = new ProductInfoData();
            return View(data);
        }

        public IActionResult IndexWithResponse(ProductInfoData data)
        {
            return View("Index", data);
        }

        [HttpPost]
        public IActionResult SendGetProductRequest([FromForm] ProductInfoData data)
        {
            var apiLogin = User.Claims.FirstOrDefault(c => c.Type == "APILogin").Value.ToString();
            var apiPassword = User.Claims.FirstOrDefault(c => c.Type == "APIPassword").Value.ToString();
            int merchantId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "MerchantId").Value.ToString());

            bool authenticated = api.Authenticate(apiLogin, apiPassword, out string jwt);

            string endpoint = "/api/products/product";

            string query = $"id={data.ProductId}";

            string response = api.GenericGet(endpoint, query, jwt);

            data.APIResponse = response;

            return RedirectToAction("IndexWithResponse", data);
        }
    }
}

using System;
using System.Linq;
using System.Net.Http;
using ForretasAPITester.API;
using ForretasAPITester.Data.Interfaces;
using ForretasAPITester.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ForretasAPITester.Controllers
{
    public class UpdateProductsController : Controller
    {
        private readonly IConfiguration Configuration;
        private readonly ForretasAPI api;
        private readonly IProductUpdateRepository ProductRepository;

        public UpdateProductsController(IConfiguration configuration, IProductUpdateRepository productUpdateRepository,
            ForretasAPI forretasAPI)
        {
            Configuration = configuration;
            ProductRepository = productUpdateRepository;
            api = forretasAPI;
        }

        public IActionResult Index()
        {
            UpdateProductsData data = new UpdateProductsData();
            return View(data);
        }

        public IActionResult IndexWithResponse(UpdateProductsData data)
        {
            return View("Index", data);
        }

        [HttpPost]
        public IActionResult SendInactivateRequest([FromForm] UpdateProductsData data)
        {
            var apiLogin = User.Claims.FirstOrDefault(c => c.Type == "APILogin").Value.ToString();
            var apiPassword = User.Claims.FirstOrDefault(c => c.Type == "APIPassword").Value.ToString();
            int merchantId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "MerchantId").Value.ToString());

            var productsToUpdate = ProductRepository.GetProducts(data, merchantId);

            var eans = productsToUpdate.Select(p => new string(p.Ean)).ToList();

            bool authenticated = api.Authenticate(apiLogin, apiPassword, out string jwt);

            string endpoint = "/api/products/inactivate";
            //endpoint = $"{Configuration.GetSection("API")?.GetSection("BaseURL").Value}/api/products/inactivate";

            string response = api.GenericPost(endpoint, eans, jwt);

            data.APIResponse = response;

            return RedirectToAction("IndexWithResponse", data);
        }

        [HttpPost]
        public IActionResult SendUpdateRequest([FromForm] UpdateProductsData data)
        {
            var apiLogin = User.Claims.FirstOrDefault(c => c.Type == "APILogin").Value.ToString();
            var apiPassword = User.Claims.FirstOrDefault(c => c.Type == "APIPassword").Value.ToString();
            int merchantId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "MerchantId").Value.ToString());

            var productsToUpdate = ProductRepository.GetProducts(data, merchantId);

            bool authenticated = api.Authenticate(apiLogin, apiPassword, out string jwt);

            string endpoint = "/api/products/update_by_ean";
             //endpoint = $"{Configuration.GetSection("API")?.GetSection("BaseURL").Value}/api/products/update_by_ean";

            string response = api.GenericPost(endpoint, productsToUpdate, jwt);

            data.APIResponse = response;

            return RedirectToAction("IndexWithResponse", data);
        }
    }
}

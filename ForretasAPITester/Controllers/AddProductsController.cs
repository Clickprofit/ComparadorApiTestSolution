using System;
using System.Text.Json;
using ForretasAPITester.API;
using ForretasAPITester.Data.Interfaces;
using ForretasAPITester.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Net.Http;

namespace ForretasAPITester.Controllers
{
    public class AddProductsController : Controller
    {
        private readonly IConfiguration Configuration;
        private readonly IProductAddRepository ProductRepository;
        private readonly ForretasAPI api;

        public AddProductsController(IConfiguration configuration, IProductAddRepository productRepository,
            ForretasAPI forretasAPI)
        {
            Configuration = configuration;
            ProductRepository = productRepository;
            api = forretasAPI;
        }

        public IActionResult Index()
        {
            AddProductsData data = new AddProductsData();
            return View(data);
        }

        public IActionResult IndexWithResponse(AddProductsData data)
        {
            return View("Index", data);
        }

        [HttpPost]
        public IActionResult SendAddRequest([FromForm] AddProductsData data)
        {
            var apiLogin = User.Claims.FirstOrDefault(c => c.Type == "APILogin").Value.ToString();
            var apiPassword = User.Claims.FirstOrDefault(c => c.Type == "APIPassword").Value.ToString();
            int merchantId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "MerchantId").Value.ToString());

            var productsToAdd = ProductRepository.GetProducts(data, merchantId);

            bool authenticated = api.Authenticate(apiLogin, apiPassword, out string jwt);

            string endpoint = "/api/products/add_by_ean";
            //endpoint = $"{Configuration.GetSection("API")?.GetSection("BaseURL").Value}/api/products/add_by_ean";

            string response = api.GenericPost(endpoint, productsToAdd, jwt);

            data.APIResponse = response;

            return RedirectToAction("IndexWithResponse", data);
        }
    }
}

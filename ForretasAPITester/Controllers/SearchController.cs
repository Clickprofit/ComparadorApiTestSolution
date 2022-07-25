using System;
using System.Linq;
using ForretasAPITester.API;
using ForretasAPITester.Models;
using Microsoft.AspNetCore.Mvc;

namespace ForretasAPITester.Controllers
{
    public class SearchController : Controller
    {
        private readonly ForretasAPI api;

        public SearchController(ForretasAPI forretasAPI)
        {
            api = forretasAPI;
        }

        public IActionResult Index()
        {
            SearchProductsData data = new SearchProductsData();
            return View(data);
        }

        public IActionResult IndexWithResponse(SearchProductsData data)
        {
            return View("Index", data);
        }

        [HttpPost]
        public IActionResult SendSearchRequest([FromForm] SearchProductsData data)
        {
            var apiLogin = User.Claims.FirstOrDefault(c => c.Type == "APILogin").Value.ToString();
            var apiPassword = User.Claims.FirstOrDefault(c => c.Type == "APIPassword").Value.ToString();
            int merchantId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "MerchantId").Value.ToString());

            bool authenticated = api.Authenticate(apiLogin, apiPassword, out string jwt);

            string endpoint = "/api/products/search";

            string query = $"offset={data.Offset}&limit={data.Limit}&title={data.Title}&ean={data.Ean}&priceMin={data.PriceMin}&priceMax={data.PriceMax}&onlyActive={data.OnlyActive}";

            string response = api.GenericGet(endpoint, query, jwt);

            data.APIResponse = response;

            return RedirectToAction("IndexWithResponse", data);
        }
    }
}

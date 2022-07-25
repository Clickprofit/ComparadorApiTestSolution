using System;
using System.Linq;
using ForretasAPITester.API;
using ForretasAPITester.Models;
using Microsoft.AspNetCore.Mvc;

namespace ForretasAPITester.Controllers
{
    public class ListProductsController : Controller
    {
        private readonly ForretasAPI api;

        public ListProductsController(ForretasAPI forretasAPI)
        {
            api = forretasAPI;
        }

        public IActionResult Index()
        {
            ListProductsData data = new ListProductsData();
            return View(data);
        }

        public IActionResult IndexWithResponse(ListProductsData data)
        {
            return View("Index", data);
        }

        [HttpPost]
        public IActionResult SendListRequest([FromForm] ListProductsData data)
        {
            var apiLogin = User.Claims.FirstOrDefault(c => c.Type == "APILogin").Value.ToString();
            var apiPassword = User.Claims.FirstOrDefault(c => c.Type == "APIPassword").Value.ToString();
            int merchantId = Convert.ToInt32(User.Claims.FirstOrDefault(c => c.Type == "MerchantId").Value.ToString());

            bool authenticated = api.Authenticate(apiLogin, apiPassword, out string jwt);

            string endpoint = "/api/products/list";

            string query = $"offset={data.Offset}&limit={data.Limit}";

            string response = api.GenericGet(endpoint, query, jwt);

            data.APIResponse = response;

            return RedirectToAction("IndexWithResponse", data);
        }
    }
}

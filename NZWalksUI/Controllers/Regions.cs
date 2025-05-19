using Microsoft.AspNetCore.Mvc;
using NZWalksUI.Models;
using System.Text.Json;

namespace NZWalksUI.Controllers
{
    public class Regions : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public Regions(IHttpClientFactory httpClientFactory) 
        {
            this._httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //GET All regions
            List<RegionDTO> response = new List<RegionDTO>();
            try
            {
                var client = _httpClientFactory.CreateClient();

                var httpResponseMessage = await client.GetAsync("https://localhost:7157/api/regions");
                httpResponseMessage.EnsureSuccessStatusCode();
                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDTO>>());
                
            }
            catch (Exception ex)
            {
                //

                throw;
            }
            
            return View(response);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(addRegionRequest addRegionRequest)
        {
            var client = _httpClientFactory.CreateClient();
            var httpResponseMessage = new HttpRequestMessage()
            {
                
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://localhost:7157/api/regions"),
                    Content = new StringContent(JsonSerializer.Serialize(addRegionRequest), System.Text.Encoding.UTF8, "application/json")
                
            };
            var httpResponse = await client.SendAsync(httpResponseMessage);
                httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDTO>();

            if(response is not null)
            {
                return RedirectToAction("Index", "Regions");
            }
            return View();
        }

        [HttpGet]

        public async Task<IActionResult> Edit(Guid id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetFromJsonAsync<RegionDTO>($"https://localhost:7157/api/regions/{id.ToString()}");
            if(response is not null){
                return View(response);
            }
            return View(null);

        }

        [HttpPost]
        public async Task<IActionResult>Edit(RegionDTO request)
        {
            var client = _httpClientFactory.CreateClient();
            var httpResponseMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7157/api/regions/{request.Id}"),
                Content = new StringContent(JsonSerializer.Serialize(request), System.Text.Encoding.UTF8, "application/json")
            };
            var httpResponse = await client.SendAsync(httpResponseMessage);
            httpResponse.EnsureSuccessStatusCode();
            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDTO>();
            if (response is not null)
            {
                return RedirectToAction("Edit", "Regions");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(RegionDTO region)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var httpResponseMesssage = await client.DeleteAsync($"https://localhost:7157/api/regions/{region.Id}");

                httpResponseMesssage.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Regions");
            }
            catch (Exception)
            {

                throw;
            }
            return View("Edit");
        }       
    }
}

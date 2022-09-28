using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MultiApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MultiApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGet()
        {
            using var httpClient = new HttpClient();
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://localhost:63385/api/jokes");
            request.Method = HttpMethod.Get;

            var response = await httpClient.SendAsync(request);

            var stream = await response.Content.ReadAsStreamAsync();

            ViewData["Joke"] = await JsonSerializer.DeserializeAsync<Joke>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}

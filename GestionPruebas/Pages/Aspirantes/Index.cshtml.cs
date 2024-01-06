using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System.Net.Http;
using Newtonsoft.Json;
using GestionPruebas.ViewModels;

namespace GestionPruebas.Pages.Aspirantes
{
    public class IndexModel : PageModel
    {
        private readonly ApiSettings _apiSettings;
        private readonly HttpClient _httpClient;

        public IndexModel(IOptions<ApiSettings> apiSettings, IHttpClientFactory httpClientFactory)
        {
            _apiSettings = apiSettings.Value;
            _httpClient = httpClientFactory.CreateClient();
        }

        public List<AspiranteViewModel> Aspirantes { get; set; }

        public async Task OnGetAsync()
        {
            var apiUrl = _apiSettings.BaseUrl + "/aspirantes";
            var response = await _httpClient.GetStringAsync(apiUrl);
            Aspirantes = JsonConvert.DeserializeObject<List<AspiranteViewModel>>(response);
        }
    }
}

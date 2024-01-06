using GestionPruebas.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;

namespace GestionPruebas.Pages.PruebaSeleccion
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

        public List<PruebaSeleccionViewModel> PruebasSeleccion { get; set; }

        public async Task OnGetAsync()
        {
            var apiUrl = _apiSettings.BaseUrl + "/pruebasseleccion";
            var response = await _httpClient.GetStringAsync(apiUrl);
            PruebasSeleccion = JsonConvert.DeserializeObject<List<PruebaSeleccionViewModel>>(response);
        }
    }
}

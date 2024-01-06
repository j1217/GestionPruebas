using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GestionPruebas.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace GestionPruebas.Pages.Aspirantes
{
    public class CreateModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public CreateModel(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [BindProperty]
        public AspiranteViewModel Aspirante { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var apiUrl = _configuration.GetValue<string>("ApiUrl") + "/aspirantes";
            var httpClient = _httpClientFactory.CreateClient();

            // Serializar el objeto AspiranteViewModel a formato JSON
            var jsonContent = JsonConvert.SerializeObject(Aspirante);

            // Crear el contenido del formulario
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Enviar la solicitud POST a la API
            var response = await httpClient.PostAsync(apiUrl, content);

            // Verificar si la solicitud fue exitosa (código de estado 2xx)
            if (response.IsSuccessStatusCode)
            {
                // Redirigir a la página de listado después de la creación
                return RedirectToPage("/Aspirantes/Index");
            }
            else
            {
                // Manejar errores aquí según sea necesario
                // Puedes acceder al contenido de la respuesta utilizando response.Content.ReadAsStringAsync()
                // y manejar los detalles del error
                ModelState.AddModelError(string.Empty, "Error al crear el aspirante.");
                return Page();
            }
        }
    }
}

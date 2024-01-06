using GestionPruebas.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace GestionPruebas.Pages.PruebaSeleccion
{
    public class CrearModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public CrearModel(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [BindProperty]
        public PreguntaViewModel Pregunta { get; set; }

        public void OnGet()
        {
            // Lógica de inicialización al cargar la página (si es necesario)
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var apiUrl = _configuration.GetValue<string>("ApiUrl") + "/preguntas";

            var httpClient = _httpClientFactory.CreateClient();

            var content = new StringContent(JsonConvert.SerializeObject(Pregunta), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                // Lógica de redirección o mensaje de éxito
                return RedirectToPage("./Index");
            }
            else
            {
                // Lógica para manejar errores (mostrar mensaje de error, redirigir a otra página, etc.)
                ModelState.AddModelError(string.Empty, "Error al guardar la pregunta.");
                return Page();
            }
        }
    }
}

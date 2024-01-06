using GestionPruebas.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace GestionPruebas.Pages.PruebaSeleccion
{
    public class EliminarModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public EliminarModel(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [BindProperty]
        public PruebaSeleccionViewModel PruebaToDelete { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var apiUrl = $"{_configuration.GetValue<string>("ApiUrl")}/pruebas/{id}";
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetStringAsync(apiUrl);
            PruebaToDelete = JsonConvert.DeserializeObject<PruebaSeleccionViewModel>(response);

            if (PruebaToDelete == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var apiUrl = $"{_configuration.GetValue<string>("ApiUrl")}/pruebas/{PruebaToDelete.ID}";
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.DeleteAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                // Lógica de redirección o mensaje de éxito
                return RedirectToPage("./Index");
            }
            else
            {
                // Lógica para manejar errores (mostrar mensaje de error, redirigir a otra página, etc.)
                ModelState.AddModelError(string.Empty, "Error al eliminar la prueba.");
                return Page();
            }
        }
    }
}

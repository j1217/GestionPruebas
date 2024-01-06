using GestionPruebas.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace GestionPruebas.Pages.Aspirantes
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
        public AspiranteViewModel AspiranteToDelete { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var apiUrl = $"{_configuration.GetValue<string>("ApiUrl")}/aspirantes/{id}";
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetStringAsync(apiUrl);
            AspiranteToDelete = JsonConvert.DeserializeObject<AspiranteViewModel>(response);

            if (AspiranteToDelete == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var apiUrl = $"{_configuration.GetValue<string>("ApiUrl")}/aspirantes/{AspiranteToDelete.ID}";
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.DeleteAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                // L�gica de redirecci�n o mensaje de �xito
                return RedirectToPage("./Index");
            }
            else
            {
                // L�gica para manejar errores (mostrar mensaje de error, redirigir a otra p�gina, etc.)
                ModelState.AddModelError(string.Empty, "Error al eliminar el aspirante.");
                return Page();
            }
        }
    }
}

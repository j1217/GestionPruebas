using GestionPruebas.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace GestionPruebas.Pages.PruebaSeleccion
{
    public class EditarModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public EditarModel(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [BindProperty]
        public PruebaSeleccionViewModel PruebaEdit { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var apiUrl = $"{_configuration.GetValue<string>("ApiUrl")}/pruebas/{id}";
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetStringAsync(apiUrl);
            PruebaEdit = JsonConvert.DeserializeObject<PruebaSeleccionViewModel>(response);

            if (PruebaEdit == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var apiUrl = $"{_configuration.GetValue<string>("ApiUrl")}/pruebas/{PruebaEdit.ID}";
            var httpClient = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(PruebaEdit), Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                // Lógica de redirección o mensaje de éxito
                return RedirectToPage("./Index");
            }
            else
            {
                // Lógica para manejar errores (mostrar mensaje de error, redirigir a otra página, etc.)
                ModelState.AddModelError(string.Empty, "Error al guardar los cambios.");
                return Page();
            }
        }
    }
}

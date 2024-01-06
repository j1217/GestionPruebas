using GestionPruebas.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace GestionPruebas.Pages.Aspirantes
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
        public AspiranteViewModel AspiranteEdit { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var apiUrl = $"{_configuration.GetValue<string>("ApiUrl")}/aspirantes/{id}";
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetStringAsync(apiUrl);
            AspiranteEdit = JsonConvert.DeserializeObject<AspiranteViewModel>(response);

            if (AspiranteEdit == null)
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

            var apiUrl = $"{_configuration.GetValue<string>("ApiUrl")}/aspirantes/{AspiranteEdit.ID}";
            var httpClient = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(AspiranteEdit), Encoding.UTF8, "application/json");
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

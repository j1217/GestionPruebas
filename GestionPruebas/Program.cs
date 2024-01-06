using GestionPruebas.Pages;

var builder = WebApplication.CreateBuilder(args);

// Configuraci�n de servicios
builder.Services.AddRazorPages();

// Configuraci�n de la base de datos
builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));

var app = builder.Build();

// Configuraci�n del pipeline de solicitudes
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

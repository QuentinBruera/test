using NegosudWeb.Components;
using NegosudWeb.Services;

var builder = WebApplication.CreateBuilder(args);

// Enregistrer ApiService en SCOPED
builder.Services.AddScoped<ApiService>(sp =>
{
    // Chaque utilisateur (circuit Blazor) aura sa propre instance
    return new ApiService("https://localhost:7024/");
});

builder.Services.AddScoped<ArticleService>(sp =>
{
    var api = sp.GetRequiredService<ApiService>();
    var supplierService = sp.GetRequiredService<SupplierService>();
    return new ArticleService(api.HttpClient, supplierService);
});

builder.Services.AddScoped<FamilyService>(sp =>
{
    var api = sp.GetRequiredService<ApiService>();
    return new FamilyService(api.HttpClient);
});

builder.Services.AddScoped<SupplierService>(sp =>
{
    var api = sp.GetRequiredService<ApiService>();
    return new SupplierService(api.HttpClient);
});

builder.Services.AddScoped<CartService>(sp =>
{
    var api = sp.GetRequiredService<ApiService>();
    return new CartService(api.HttpClient);
});

//// Ajout du service client de l'API avec pré-authentification
//var apiService = new ApiService("https://localhost:7024/");
//var loginResponse = await apiService.Login("admin@gmail.com", "Admin1!");
//if (!loginResponse)
//{
//    throw new Exception("Authentication failed");
//}
//builder.Services.AddScoped<ApiService>(_ => apiService);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

using ProduitFamilleMVC_SQLServer.Models;
using ProduitFamilleMVC_SQLServer.Models.Repositories;
using ProduitFamilleMVC_SQLServer.ViewModels;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IRepository<Famille>, FamilleRepository>();
builder.Services.AddSingleton<IRepository<ProduitFamilleViewModel>, ProduitRepository>();
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();


var builder2 = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
IConfiguration _configuration = builder2.Build();
var connexion = _configuration.GetConnectionString("Prod-Famille-context");
Global.cc = connexion;




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

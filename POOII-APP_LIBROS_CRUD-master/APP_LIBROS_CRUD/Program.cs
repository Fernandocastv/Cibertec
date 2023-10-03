using APP_LIBROS_CRUD.Models;
using APP_LIBROS_CRUD.Repositorios.Contrato;
using APP_LIBROS_CRUD.Repositorios.Implementación;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IGenericRepository<Autor>, AutorRepository>();
builder.Services.AddScoped<IGenericRepository<Editorial>, EditorialRepository>();
builder.Services.AddScoped<IGenericRepository<Libro>, LibroRepository>();

var app = builder.Build();

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

using Microsoft.EntityFrameworkCore;
using MyStore.Models;
using MyStore.Models.Repository;

var builder = WebApplication.CreateBuilder(args);

#region Services

builder.Services.AddMvc();

builder.Services.AddDbContext<MyStoreDataContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:MyStore"]);
});

builder.Services.AddScoped<IProductRepository, EFProductRepository>();
builder.Services.AddScoped<ICategoryRepository, EFCategoryRepository>();
builder.Services.AddScoped<ISupplierRepository, EFSupplierRepository>();


builder.Services.AddRazorPages();

#endregion

var app = builder.Build();


#region App

if (app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler();
}

app.UseStaticFiles();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllerRoute(
    name: "pagination",
    pattern: "Products/page{productPage}",
    defaults: new {Controller = "Home", Action = "Index"});

app.MapDefaultControllerRoute();


#endregion

SeedData.EnsurePopulated(app);

app.Run();

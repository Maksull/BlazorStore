using Microsoft.EntityFrameworkCore;
using MyStore.Models;
using MyStore.Models.Cart;
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

builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddRazorPages();

#endregion

var app = builder.Build();


#region App

if (app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler();
}

app.UseStaticFiles();

app.UseSession();

app.UseAuthorization();
app.UseAuthentication();



app.MapRazorPages();

app.MapControllerRoute(
    name: "categoryPage",
    pattern: "{category}/Page{productPage:int}",
    defaults: new {Controller = "Home", Action = "Index"});

app.MapControllerRoute(
    name: "page",
    pattern: "Page{productPage:int}",
    defaults: new {Controller = "Home", Action = "Index"});

app.MapControllerRoute(
    name: "category",
    pattern: "{category}",
    defaults: new {Controller = "Home", Action = "Index"});

app.MapControllerRoute(
    name: "pagination",
    pattern: "Products/page{productPage:int}",
    defaults: new {Controller = "Home", Action = "Index"});

app.MapDefaultControllerRoute();


#endregion

SeedData.EnsurePopulated(app);

app.Run();

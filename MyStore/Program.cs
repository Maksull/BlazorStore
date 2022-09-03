using Microsoft.AspNetCore.Identity;
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

builder.Services.AddDbContext<IdentityDataContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:MyStoreIdentity"]);
});
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<IdentityDataContext>();

builder.Services.AddScoped<IProductRepository, EFProductRepository>();
builder.Services.AddScoped<ICategoryRepository, EFCategoryRepository>();
builder.Services.AddScoped<ISupplierRepository, EFSupplierRepository>();
builder.Services.AddScoped<IOrderRepository, EFOrderRepository>();
builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();

builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddRazorPages();

builder.Services.AddServerSideBlazor();

#endregion

var app = builder.Build();


#region App

if (app.Environment.IsProduction())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();


app.MapBlazorHub();
app.MapFallbackToPage("/admin/{*catchall}", "/admin/index");

app.MapRazorPages();



app.MapControllerRoute(
    name: "categorySortAndSearch",
    pattern: "{category}/Page{productPage:int}/SortBy{sortBy}/SearchBy{searchBy}",
    defaults: new { Controller = "Home", Action = "Index" });

app.MapControllerRoute(
    name: "categorySort",
    pattern: "{category}/Page{productPage:int}/SortBy{sortBy}",
    defaults: new { Controller = "Home", Action = "Index" });

app.MapControllerRoute(
    name: "categorySearch",
    pattern: "{category}/Page{productPage:int}/SearchBy{searchBy}",
    defaults: new { Controller = "Home", Action = "Index" });



app.MapControllerRoute(
    name: "sortAndSearch",
    pattern: "Page{productPage:int}/SortBy{sortBy}/SearchBy{searchBy}",
    defaults: new { Controller = "Home", Action = "Index" });

app.MapControllerRoute(
    name: "sort",
    pattern: "Page{productPage:int}/SortBy{sortBy}",
    defaults: new { Controller = "Home", Action = "Index" });

app.MapControllerRoute(
    name: "search",
    pattern: "Page{productPage:int}/SearchBy{searchBy}",
    defaults: new { Controller = "Home", Action = "Index" });



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
IdentitySeedData.EnsurePopulated(app);

app.Run();

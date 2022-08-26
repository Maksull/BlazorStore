using Microsoft.EntityFrameworkCore;

namespace MyStore.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            MyStoreDataContext context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<MyStoreDataContext>();

            context.Database.Migrate();
            if(context.Products.Count() == 0 && context.Categories.Count() == 0 && context.Suppliers.Count() == 0)
            {
                Supplier s1 = new Supplier { Name = "Splash Dudes", City = "San Jose" };
                Supplier s2 = new Supplier { Name = "Soccer Town", City = "Chicago" };
                Supplier s3 = new Supplier { Name = "Chess Co", City = "New York" };

                Category c1 = new Category { Name = "Watersports" };
                Category c2 = new Category { Name = "Soccer" };
                Category c3 = new Category { Name = "Chess" };

                context.Products.AddRange(
                    new Product
                    {
                        Name = "Kayak",
                        Description = "A boat for one person",
                        Price = 275,
                        Category = c1,
                        Supplier = s1
                    },
                    new Product
                    {
                        Name = "Lifejacket",
                        Description = "Protective and fashionable",
                        Price = 48.95m,
                        Category = c1,
                        Supplier = s1
                    },
                    new Product
                    {
                        Name = "Soccer Ball",
                        Description = "FIFA-approved size and weight",
                        Price = 19.50m,
                        Category = c2,
                        Supplier = s2
                    },
                    new Product
                    {
                        Name = "Corner Flags",
                        Description = "Give your playing field a professional touch",
                        Price = 34.95m,
                        Category = c2,
                        Supplier = s2
                    },
                    new Product
                    {
                        Name = "Stadium",
                        Description = "Flat-packed 35,000-seat stadium",
                        Price = 79500,
                        Category = c2,
                        Supplier = s2
                    },
                    new Product
                    {
                        Name = "Thinking Cap",
                        Description = "Improve brain efficiency by 75%",
                        Price = 16,
                        Category = c3,
                        Supplier = s3
                    },
                    new Product
                    {
                        Name = "Unsteady Chair",
                        Description = "Secretly give your opponent a disadvantage",
                        Price = 29.95m,
                        Category = c3,
                        Supplier = s3
                    },
                    new Product
                    {
                        Name = "Human Chess Board",
                        Description = "A fun game for the family",
                        Price = 75,
                        Category = c3,
                        Supplier = s3
                    },
                    new Product
                    {
                        Name = "Bling-Bling King",
                        Description = "Gold-plated, diamond-studded King",
                        Price = 1200,
                        Category = c3,
                        Supplier = s3
                    }
                );
            }
            context.SaveChanges();
        }
    }
}

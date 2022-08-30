namespace MyStore.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void Can_Filter_Product()
        {
            //Arrange
            Mock<IProductRepository> mock = new();

            Category category1 = new Category
            {
                Name = "One"
            };
            Category category2 = new Category
            {
                Name = "Two"
            };

            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductId = 1, Name = "P1", Category = category1},
                new Product {ProductId = 2, Name = "P2", Category = category2},
                new Product {ProductId = 3, Name = "P3", Category = category2},
                new Product {ProductId = 4, Name = "P4", Category = category1},
                new Product {ProductId = 5, Name = "P5", Category = category1}
            }.AsQueryable());

            HomeController controller = new(mock.Object);


            //Action
            Product[] result = ((controller.Index("One", 1) as ViewResult)?.ViewData.Model as ProductsListViewModel ?? new()).Products.ToArray();


            //Assert
            Assert.Equal(3, result.Length);
            Assert.True(result[0].Name == "P1" && result[0].Category?.Name == "One");
            Assert.True(result[1].Name == "P4" && result[1].Category?.Name == "One");
        }

        [Fact]
        public void Can_Use_Products()
        {
            //Arrange
            Mock<IProductRepository> mock = new();
            Category category1 = new Category
            {
                Name = "One"
            };

            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product {ProductId = 1, Name = "P1", Category = category1},
                new Product {ProductId = 2, Name = "P2", Category = category1}
            }).AsQueryable());

            HomeController controller = new(mock.Object);


            //Action
            Product[] result = ((controller.Index(null) as ViewResult)?.ViewData.Model as ProductsListViewModel ?? new()).Products.ToArray();

            //Assert
            Assert.True(result.Length == 2);
            Assert.Equal("P1", result[0].Name);
            Assert.Equal("P2", result[1].Name);
        }

        [Fact]
        public void Can_Paginating()
        {
            //Arrange
            Mock<IProductRepository> mock = new();
            Category category1 = new Category
            {
                Name = "One"
            };

            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product { ProductId = 1, Name = "P1", Category = category1},
                new Product { ProductId = 2, Name = "P2", Category = category1},
                new Product { ProductId = 3, Name = "P3", Category = category1},
                new Product { ProductId = 4, Name = "P4", Category = category1},
                new Product { ProductId = 5, Name = "P5", Category = category1}
            }).AsQueryable());

            HomeController controller = new(mock.Object)
            {
                PageSize = 3
            };

            //Action

            ProductsListViewModel result = (controller.Index(null, 2) as ViewResult)?.ViewData.Model as ProductsListViewModel ?? new();
            Product[] products = result.Products.ToArray();
            PagingInfo pagingInfo = result.PagingInfo;

            //Assert
            Assert.Equal(2, pagingInfo.CurrentPage);
            Assert.Equal(3, pagingInfo.ItemsPerPage);
            Assert.Equal(5, pagingInfo.TotalItems);
            Assert.Equal(2, pagingInfo.TotalPages);

            Assert.True(products.Length == 2);
            Assert.Equal("P4", products[0].Name);
            Assert.Equal("P5", products[1].Name);
        }

        [Fact]
        public void Can_Specify_Category()
        {
            //Arrange
            Mock<IProductRepository> mock = new();

            Category category1 = new Category
            {
                Name = "One"
            };
            Category category2 = new Category
            {
                Name = "Two"
            };

            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product {ProductId = 1, Name = "P1", Category = category1},
                new Product {ProductId = 2, Name = "P2", Category = category2},
                new Product {ProductId = 3, Name = "P3", Category = category2},
                new Product {ProductId = 4, Name = "P4", Category = category1},
                new Product {ProductId = 5, Name = "P5", Category = category1}
            }).AsQueryable());

            HomeController controller = new(mock.Object)
            {
                PageSize = 2
            };

            //Action
            ProductsListViewModel result = (controller.Index("One", 2) as ViewResult)?.ViewData.Model as ProductsListViewModel ?? new();
            Product[] products = result.Products.ToArray();
            PagingInfo pagingInfo = result.PagingInfo;

            //Assert
            Assert.True(products.Length == 1);
            Assert.Equal("P5", products[0].Name);

            Assert.Equal(2, pagingInfo.CurrentPage);
            Assert.Equal(2, pagingInfo.ItemsPerPage);
            Assert.Equal(2, pagingInfo.TotalPages);
            Assert.Equal(3, pagingInfo.TotalItems);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace MyStore.Tests
{
    public class CartRazorPageTests
    {
        [Fact]
        public void Can_Load_Cart()
        {
            //Arrange
            Product p1 = new() { ProductId = 1, Name = "First" };
            Product p2 = new() { ProductId = 2, Name = "Second" };

            Cart cart = new();

            Mock<IProductRepository> mock = new();
            mock.Setup(m => m.Products).Returns((new Product[] { p1, p2 }).AsQueryable());

            //Action
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 1);

            CartModel cartModel = new(mock.Object, cart);

            cartModel.OnGet("returnUrl");

            //Assert
            Assert.Equal(2, cartModel.Cart.Lines.Count);
            Assert.Equal("returnUrl", cartModel.ReturnUrl);

        }

        [Fact]
        public void Can_Update_Cart()
        {
            //Arrange
            Mock<IProductRepository> mock = new();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product { ProductId = 1, Name = "First" }
            }).AsQueryable());

            Cart cart = new();


            //Action
            CartModel cartModel = new(mock.Object, cart);
            cartModel.OnPost(1, "returnUrl");

            //Assert
            Assert.Single(cartModel.Cart.Lines);
            Assert.Equal("First", cartModel.Cart.Lines.First().Product.Name);
            Assert.Equal(1, cartModel.Cart.Lines.First().Quantity);
        }
    }
}

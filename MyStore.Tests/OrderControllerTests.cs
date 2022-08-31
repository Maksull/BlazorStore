using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Tests
{
    public class OrderControllerTests
    {
        [Fact]
        public void Cannot_Checkout_Empty_Cart()
        {
            //Arrange
            Mock<IOrderRepository> mock = new();
            Cart cart = new();
            Order order = new();
            OrderController controller = new(mock.Object, cart);


            //Action
            ViewResult? result = controller.Checkout(order) as ViewResult;


            //Assert
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);

            Assert.True(string.IsNullOrEmpty(result?.ViewName));
            Assert.False(result?.ViewData.ModelState.IsValid);
        }


        [Fact]
        public void Cannot_Checkout_Invalid_ShippingDetails()
        {
            //Arrange
            Mock<IOrderRepository> mock = new();
            Cart cart = new();
            cart.AddItem(new Product(), 1);

            OrderController controller = new(mock.Object, cart);
            controller.ModelState.AddModelError("Error", "Error");

            //Action
            ViewResult? result = controller.Checkout(new Order()) as ViewResult;


            //Assert
            mock.Verify(m => m.SaveOrder(It.IsAny<Order>()), Times.Never);
            Assert.True(string.IsNullOrEmpty(result?.ViewName));
            Assert.False(result?.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void Can_Checkout_Submit_Order()
        {
            //Arrange
            Mock<IOrderRepository> mock = new();
            Cart cart = new();
            cart.AddItem(new Product(), 1);

            OrderController controller = new(mock.Object, cart);

            //Action
            RedirectToPageResult? result = controller.Checkout(new Order()) as RedirectToPageResult;


            //Assert
            Assert.Equal("/Completed", result?.PageName);
        }
    }
}

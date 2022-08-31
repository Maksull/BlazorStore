namespace MyStore.Tests
{
    public class CartTests
    {
        [Fact]
        public void Can_Add_New_Lines()
        {
            //Arrange
            Product p1 = new() { ProductId = 1, Name = "First" };
            Product p2 = new() { ProductId = 2, Name = "Second" };

            Cart cart = new();

            //Action
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 1);
            CartLine[] cartLines = cart.Lines.ToArray();

            //Assert
            Assert.Equal(2, cartLines.Length);
            Assert.Equal(p1, cartLines[0].Product);
            Assert.Equal(p2, cartLines[1].Product);
        }

        [Fact]
        public void Can_Increase_Qunatity()
        {
            //Arrange
            Product p1 = new() { ProductId = 1, Name = "First" };
            Product p2 = new() { ProductId = 2, Name = "Second" };

            Cart cart = new();

            //Action
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 2);
            cart.AddItem(p1, 11);

            CartLine[] cartLines = cart.Lines.ToArray();

            //Assert
            Assert.Equal(2, cartLines.Length);
            Assert.Equal(12, cartLines[0].Quantity);
            Assert.Equal(2, cartLines[1].Quantity);
        }

        [Fact]
        public void Can_Remove_Line()
        {
            //Arrange
            Product p1 = new() { ProductId = 1, Name = "First" };
            Product p2 = new() { ProductId = 2, Name = "Second" };

            Cart cart = new();

            //Action
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 3);
            cart.AddItem(p1, 1);
            cart.RemoveLine(p1);


            //Assert
            Assert.Empty(cart.Lines.Where(cl => cl.Product == p1));
            Assert.True(cart.Lines.Count == 1);
        }

        [Fact]
        public void Calculate_Cart_Value()
        {
            //Arrange
            Product p1 = new() { ProductId = 1, Name = "First", Price = 100M };
            Product p2 = new() { ProductId = 2, Name = "Second", Price = 50M };

            Cart cart = new Cart();

            //Action
            cart.AddItem(p1, 2);
            cart.AddItem(p2, 3);
            decimal value = cart.ComputeTotalValue();

            //Assert
            Assert.Equal(350, value);
        }

        [Fact]
        public void Can_Clear_Content()
        {
            //Arrange
            Product p1 = new() { ProductId = 1, Name = "First" };
            Product p2 = new() { ProductId = 2, Name = "Second" };

            Cart cart = new();


            //Action
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 1);
            cart.Clear();

            //Assert
            Assert.Empty(cart.Lines);
        }

    }
}

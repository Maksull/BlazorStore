namespace MyStore.Models.Cart
{
    public class Cart
    {
        public List<CartLine> Lines { get; set; } = new();

        public virtual void AddItem(Product product, int quantity)
        {
            CartLine? cartLine = Lines.Where(l => l.Product.ProductId == product.ProductId).FirstOrDefault();

            if(cartLine == null)
            {
                Lines.Add(new CartLine { Product = product, Quantity = quantity});
            }
            else
            {
                cartLine.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(Product product) => Lines.RemoveAll(l => l.Product.ProductId == product.ProductId);

        public decimal ComputeTotalValue() => Lines.Sum(l => l.Product.Price * l.Quantity);

        public virtual void Clear() => Lines.Clear();
    }
}

namespace SA_Online_Mart.Models
{
    public class Cart
    {
        public List<CartItem> Items { get; set; } = new List<CartItem>();

        public void AddItem(Product product)
        {
            var item = Items.FirstOrDefault(i => i.Product.Id == product.Id);
            if (item != null)
            {
                item.Quantity++;
            }
            else
            {
                Items.Add(new CartItem { Product = product, Quantity = 1 });
            }
        }

        public void RemoveItem(int productId)
        {
            var item = Items.FirstOrDefault(i => i.Product.Id == productId);
            if (item != null)
            {
                Items.Remove(item);
            }
        }

        public decimal TotalValue => Items.Sum(i => Convert.ToDecimal(i.Product.Price) * i.Quantity);
    }

    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}

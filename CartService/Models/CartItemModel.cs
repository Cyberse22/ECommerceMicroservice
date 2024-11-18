namespace CartService.Models
{
    public class CartItemModel
    {
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}

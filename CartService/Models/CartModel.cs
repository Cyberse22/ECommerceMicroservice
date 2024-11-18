namespace CartService.Models
{
    public class CartModel
    {
        public string UserId { get; set; }
        public List<CartItemModel> Items { get; set; } = new List<CartItemModel>();
    }
}

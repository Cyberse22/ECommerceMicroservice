namespace OrderService.Data
{
    public class OrderItem : BaseEntity
    {
        public string? OrderItemId { get; set; }
        public string? OrderId { get; set; }
        public string? ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? Discount { get; set; }
    }
}

namespace OrderService.Data
{
    public class Order : BaseEntity
    {
        public string? OrderId { get; set; }
        public string? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerPhone { get; set; }
        public string? PaymentMethod { get; set; }
        public decimal? TotalAmount { get; private set; }
        public string Currency { get; set; } = "VND";
    }
}

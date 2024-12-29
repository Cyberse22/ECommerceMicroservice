namespace OrderService.Data
{
    public class ShippingDetails : BaseEntity
    {
        public string? ShippingId { get; set; }
        public string? OrderId { get; set; }
        public string? RecipientName { get; set; }
        public string? Phone {  get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? City { get; set; }
        public string? State {  get; set; }
        public string? Country { get; set; }
        // Method Delivery: COD, Any...
        public string? ShippingMethod { get; set; }
        // Order Tracking
        public string? TrackingNumber { get; set; }
        public DateTime? EstimatedDate {  get; set; }
    }
}

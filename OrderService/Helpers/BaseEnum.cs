namespace OrderService.Helpers
{
    public class BaseEnum
    {
        public enum OrderStatus
        {
            Created,
            Confirmed,
            Processing,
            Shipped,
            Cancelled,
            Delivered,
            Returned,
        }
        public enum PaymentStatus
        {
            Pending,
            Processing,
            Completed,
            Failed,
            Refunded
        }
        public enum ShippingStatus
        {
            Pending,
            Processing, 
            Shipped,
            Delivered,
            Failed
        }
    }
}

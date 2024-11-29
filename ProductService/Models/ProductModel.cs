namespace ProductService.Models
{
    public class ProductModel
    {
        public string? ProductId { get; set; }
        public string? ProductName { get; set; }
        public string ProductDescription { get; set; } = string.Empty;
        public decimal? ProductPrice { get; set; }
        public bool? IsActive { get; set; } = true;
    }
}

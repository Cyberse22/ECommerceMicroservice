using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProductService.Data
{
    public class Product : BaseEntity
    {
        [BsonElement]
        public string? ProductId { get; set; }
        [BsonElement]
        public string? ProductName { get; set; }
        [BsonElement]
        public string ProductDescription { get; set; } = string.Empty;
        [BsonElement]
        public decimal? ProductPrice { get; set; }
    }
}

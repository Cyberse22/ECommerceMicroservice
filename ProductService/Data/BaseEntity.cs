using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProductService.Data
{
    [BsonIgnoreExtraElements]
    public class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        [BsonElement("createddate")]
        public DateOnly CreatedDate { get; set; }
        [BsonElement("updateddate")]
        public DateOnly UpdatedDate { get; set; } = new DateOnly();
        public bool IsActive { get; set; }
    }
}

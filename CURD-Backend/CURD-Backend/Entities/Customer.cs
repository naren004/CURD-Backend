using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApiMongoDbDemo.Entities
{
    public class Customer
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("customer_name"), BsonRepresentation(BsonType.String)]
        public string? CustomernName { get; set; }

        [BsonElement("email"), BsonRepresentation(BsonType.String)]
        public string? Email { get; set; }

        [BsonElement("password"), BsonRepresentation(BsonType.String)]
        public string? Password { get; set; }
    }
}

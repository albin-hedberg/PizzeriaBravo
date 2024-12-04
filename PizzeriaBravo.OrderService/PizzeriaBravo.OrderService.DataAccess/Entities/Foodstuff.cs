using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PizzeriaBravo.OrderService.DataAccess.Entities;

public class Foodstuff
{
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
}

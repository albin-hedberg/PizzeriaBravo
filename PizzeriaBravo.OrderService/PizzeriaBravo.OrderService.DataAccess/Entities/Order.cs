using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PizzeriaBravo.OrderService.DataAccess.Enums;

namespace PizzeriaBravo.OrderService.DataAccess.Entities;

public class Order
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    [BsonRepresentation(BsonType.String)]
    public Guid CustomerId { get; set; }
    public List<Pizza>? Pizzas { get; set; }
    public List<Foodstuff>? Foodstuff { get; set; }
    public OrderStatus Status { get; set; }
}

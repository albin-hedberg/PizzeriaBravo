using MongoDB.Driver;
using PizzeriaBravo.OrderService.DataAccess.Entities;
using PizzeriaBravo.OrderService.DataAccess.Enums;
using PizzeriaBravo.OrderService.DataAccess.Interfaces;
using PizzeriaBravo.OrderService.DataAccess.Utilities;

namespace PizzeriaBravo.OrderService.DataAccess.Repositories;

public class OrderRepository : IOrderService<Order>
{
    private readonly IMongoCollection<Order> _orders;

    public OrderRepository()
    {
        var database = MongoDbConnection.GetDatabase();
        _orders = database.GetCollection<Order>("Orders", new MongoCollectionSettings() { AssignIdOnInsert = true });
    }

    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await _orders.Find(order => true).ToListAsync();
    }

    public async Task<Order> GetOrderByIdAsync(Guid id)
    {
        return await _orders.Find(order => order.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(Guid customerId)
    {
        return await _orders.Find(order => order.CustomerId == customerId).ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetOrdersByOrderStatusAsync(OrderStatus status)
    {
        return await _orders.Find(order => order.Status == status).ToListAsync();
    }

    public async Task<Order> CreateOrderAsync(Order order)
    {
        await _orders.InsertOneAsync(order);
        return order;
    }

    public async Task<Order> UpdateOrderStatusAsync(Guid id, OrderStatus status)
    {
        var filter = Builders<Order>.Filter.Eq(order => order.Id, id);
        var update = Builders<Order>.Update.Set(order => order.Status, status);
        return await _orders.FindOneAndUpdateAsync(filter, update);
    }

    public async Task DeleteOrderAsync(Guid id)
    {
        await _orders.DeleteOneAsync(order => order.Id == id);
    }
}

using PizzeriaBravo.OrderService.DataAccess.Enums;

namespace PizzeriaBravo.OrderService.DataAccess.Interfaces;

public interface IOrderService<T> where T : class
{
    Task<IEnumerable<T>> GetAllOrdersAsync();
    Task<T> GetOrderByIdAsync(Guid id);
    Task<IEnumerable<T>> GetOrdersByCustomerIdAsync(Guid id);
    Task<IEnumerable<T>> GetOrdersByOrderStatusAsync(OrderStatus status);
    Task<T> CreateOrderAsync(T entity);
    Task<T> UpdateOrderStatusAsync(Guid id, OrderStatus status);
    Task DeleteOrderAsync(Guid id);
}

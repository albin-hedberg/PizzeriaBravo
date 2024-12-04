using PizzeriaBravo.OrderService.DataAccess.Entities;
using PizzeriaBravo.OrderService.DataAccess.Enums;

namespace PizzeriaBravo.OrderService.DataAccess.Interfaces;

public interface IOrderService<T> where T : class
{
    Task<Response<IEnumerable<T>>> GetAllOrdersAsync();
    Task<Response<T>> GetOrderByIdAsync(Guid id);
    Task<Response<IEnumerable<T>>> GetOrdersByCustomerIdAsync(Guid id);
    Task<Response<IEnumerable<T>>> GetOrdersByOrderStatusAsync(OrderStatus status);
    Task<Response<T>> CreateOrderAsync(T entity);
    Task<Response<T>> UpdateOrderStatusAsync(Guid id, OrderStatus status);
    Task<Response<Guid>> DeleteOrderAsync(Guid id);
}

﻿using MongoDB.Driver;
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

    public async Task<Response<IEnumerable<Order>>> GetAllOrdersAsync()
    {
        try
        {
            var orders = await _orders.Find(order => true).ToListAsync();

            if (!orders.Any())
            {
                return new Response<IEnumerable<Order>>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "No orders found."
                };
            }

            return new Response<IEnumerable<Order>>
            {
                Data = orders,
                IsSuccess = true,
                Message = "Orders retrieved successfully."
            };
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<Order>>
            {
                Data = null,
                IsSuccess = false,
                Message = $"Error: {ex.Message}"
            };
        }
        //return await _orders.Find(order => true).ToListAsync();
    }

    public async Task<Response<Order>> GetOrderByIdAsync(Guid id)
    {
        try
        {
            var order = await _orders.Find(o => o.Id == id).FirstOrDefaultAsync();
            if (order == null)
            {
                return new Response<Order>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "Order not found."
                };
            }
            return new Response<Order>
            {
                Data = order,
                IsSuccess = true,
                Message = "Order retrieved successfully."
            };
        }
        catch (Exception ex)
        {
            return new Response<Order>
            {
                Data = null,
                IsSuccess = false,
                Message = $"Error: {ex.Message}"
            };
        }
    }

    public async Task<Response<IEnumerable<Order>>> GetOrdersByCustomerIdAsync(Guid customerId)
    {
        try
        {
            var orders = await _orders.Find(o => o.CustomerId == customerId).ToListAsync();
            if (!orders.Any())
            {
                return new Response<IEnumerable<Order>>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "No orders found for this customer."
                };
            }
            return new Response<IEnumerable<Order>>
            {
                Data = orders,
                IsSuccess = true,
                Message = "Orders retrieved successfully."
            };
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<Order>>
            {
                Data = null,
                IsSuccess = false,
                Message = $"Error: {ex.Message}"
            };
        }
    }

    public async Task<Response<IEnumerable<Order>>> GetOrdersByOrderStatusAsync(OrderStatus status)
    {
        try
        {
            var orders = await _orders.Find(o => o.Status == status).ToListAsync();
            if (!orders.Any())
            {
                return new Response<IEnumerable<Order>>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "No orders found with this status."
                };
            }
            return new Response<IEnumerable<Order>>
            {
                Data = orders,
                IsSuccess = true,
                Message = "Orders retrieved successfully."
            };
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<Order>>
            {
                Data = null,
                IsSuccess = false,
                Message = $"Error: {ex.Message}"
            };
        }
    }

    public async Task<Response<Order>> CreateOrderAsync(Order order)
    {
        try
        {
            await _orders.InsertOneAsync(order);
            return new Response<Order>
            {
                Data = order,
                IsSuccess = true,
                Message = "Order created successfully."
            };
        }
        catch (Exception ex)
        {
            return new Response<Order>
            {
                Data = null,
                IsSuccess = false,
                Message = $"Error: {ex.Message}"
            };
        }
    }

    public async Task<Response<Order>> UpdateOrderStatusAsync(Guid id, OrderStatus status)
    {
        try
        {
            var update = Builders<Order>.Update.Set(o => o.Status, status);
            var result = await _orders.UpdateOneAsync(o => o.Id == id, update);
            if (result.MatchedCount == 0)
            {
                return new Response<Order>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "Order not found."
                };
            }
            var updatedOrder = await _orders.Find(o => o.Id == id).FirstOrDefaultAsync();
            return new Response<Order>
            {
                Data = updatedOrder,
                IsSuccess = true,
                Message = "Order status updated successfully."
            };
        }
        catch (Exception ex)
        {
            return new Response<Order>
            {
                Data = null,
                IsSuccess = false,
                Message = $"Error: {ex.Message}"
            };
        }
    }

    public async Task<Response<Guid>> DeleteOrderAsync(Guid id)
    {
        try
        {
            var result = await _orders.DeleteOneAsync(o => o.Id == id);
            if (result.DeletedCount == 0)
            {
                return new Response<Guid>
                {
                    Data = id,
                    IsSuccess = false,
                    Message = "Order not found."
                };
            }
            return new Response<Guid>
            {
                Data = id,
                IsSuccess = true,
                Message = "Order deleted successfully."
            };
        }
        catch (Exception ex)
        {
            return new Response<Guid>
            {
                Data = id,
                IsSuccess = false,
                Message = $"Error: {ex.Message}"
            };
        }
    }

    //public async Task<Order> GetOrderByIdAsync(Guid id)
    //{
    //    return await _orders.Find(order => order.Id == id).FirstOrDefaultAsync();
    //}

    //public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(Guid customerId)
    //{
    //    return await _orders.Find(order => order.CustomerId == customerId).ToListAsync();
    //}

    //public async Task<IEnumerable<Order>> GetOrdersByOrderStatusAsync(OrderStatus status)
    //{
    //    return await _orders.Find(order => order.Status == status).ToListAsync();
    //}

    //public async Task<Order> CreateOrderAsync(Order order)
    //{
    //    await _orders.InsertOneAsync(order);
    //    return order;
    //}

    //public async Task<Order> UpdateOrderStatusAsync(Guid id, OrderStatus status)
    //{
    //    var filter = Builders<Order>.Filter.Eq(order => order.Id, id);
    //    var update = Builders<Order>.Update.Set(order => order.Status, status);
    //    return await _orders.FindOneAndUpdateAsync(filter, update);
    //}

    //public async Task DeleteOrderAsync(Guid id)
    //{
    //    await _orders.DeleteOneAsync(order => order.Id == id);
    //}
}

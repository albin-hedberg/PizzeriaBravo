using Microsoft.AspNetCore.Mvc;
using PizzeriaBravo.OrderService.API.Dto;
using PizzeriaBravo.OrderService.API.Interfaces;
using PizzeriaBravo.OrderService.DataAccess.Entities;
using PizzeriaBravo.OrderService.DataAccess.Enums;
using PizzeriaBravo.OrderService.DataAccess.Interfaces;

namespace PizzeriaBravo.OrderService.API.Extensions;

public static class OrderEndpointExtension
{
    public static IEndpointRouteBuilder MapOrderEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/orders");

        group.MapGet("/", GetAllOrders);
        group.MapGet("/{id}", GetOrderById);
        group.MapPost("/", CreateOrder);
        group.MapPut("/{id}/status/{status}", UpdateOrderStatus);
        group.MapDelete("/{id}", CancelOrder);

        return app;
    }

    private static async Task<IResult> GetAllOrders(IOrderService<Order> repo)
    {
        var response = await repo.GetAllOrdersAsync();

        if (!response.IsSuccess)
        {
            return Results.NotFound(response);
        }

        return Results.Ok(response);
    }

    private static async Task<IResult> GetOrderById(IOrderService<Order> repo, Guid id)
    {
        var response = await repo.GetOrderByIdAsync(id);
        if (!response.IsSuccess)
        {
            return Results.NotFound(response);
        }
        return Results.Ok(response);
    }

    private static async Task<IResult> CreateOrder(IMessageService ms, [FromBody] Order order)
    {
        var message = new MessageDto<Order>
        {
            MethodInfo = "post",
            Data = order
        };

        await ms.PublishMessageAsync(message);

        return Results.Ok(message);

        //var response = await repo.CreateOrderAsync(order);
        //if (!response.IsSuccess)
        //{
        //    return Results.BadRequest(response);
        //}
        //return Results.Created($"/api/orders/{response.Data.Id}", response);
    }

    private static async Task<IResult> UpdateOrderStatus(IOrderService<Order> repo, Guid id, OrderStatus status)
    {

        var response = await repo.UpdateOrderStatusAsync(id, status);
        if (!response.IsSuccess)
        {
            return Results.NotFound(response);
        }
        return Results.Ok(response);
    }

    private static async Task<IResult> CancelOrder(IOrderService<Order> repo, Guid id)
    {
        var response = await repo.UpdateOrderStatusAsync(id, OrderStatus.Cancelled);
        if (!response.IsSuccess)
        {
            return Results.NotFound(response);
        }
        return Results.Ok(response);
    }

    //private static async Task<IResult> DeleteOrder(IOrderService<Order> repo, Guid id)
    //{
    //    var response = await repo.DeleteOrderAsync(id);
    //    if (!response.IsSuccess)
    //    {
    //        return Results.NotFound(response);
    //    }
    //    return Results.Ok(response);
    //}
}

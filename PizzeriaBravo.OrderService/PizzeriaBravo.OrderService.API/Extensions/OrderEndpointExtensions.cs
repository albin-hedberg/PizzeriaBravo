using Microsoft.AspNetCore.Mvc;
using PizzeriaBravo.OrderService.DataAccess.Entities;
using PizzeriaBravo.OrderService.DataAccess.Enums;
using PizzeriaBravo.OrderService.DataAccess.Repositories;

namespace PizzeriaBravo.OrderService.API.Extensions;

public static class OrderEndpointExtension
{
    public static IEndpointRouteBuilder MapOrderEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/orders");

        group.MapGet("/", GetAllOrders);
        group.MapGet("/{id}", GetOrderById);
        group.MapGet("/customer/{id}", GetOrdersByCustomerId);
        group.MapGet("/status/{status}", GetOrdersByStatus);
        group.MapPost("/", CreateOrder);
        group.MapPut("/{id}/status/{status}", UpdateOrderStatus);
        group.MapDelete("/{id}", DeleteOrder);

        return app;
    }

    private static async Task<IResult> GetAllOrders([FromServices] OrderRepository repo)
    {
        var response = await repo.GetAllOrdersAsync();

        if (!response.IsSuccess)
        {
            return Results.NotFound(response);
        }

        return Results.Ok(response);
    }

    private static async Task<IResult> GetOrderById([FromServices] OrderRepository repo, Guid id)
    {
        var response = await repo.GetOrderByIdAsync(id);
        if (!response.IsSuccess)
        {
            return Results.NotFound(response);
        }
        return Results.Ok(response);
    }

    private static async Task<IResult> GetOrdersByCustomerId([FromServices] OrderRepository repo, Guid id)
    {
        var response = await repo.GetOrdersByCustomerIdAsync(id);
        if (!response.IsSuccess)
        {
            return Results.NotFound(response);
        }
        return Results.Ok(response);
    }

    private static async Task<IResult> GetOrdersByStatus([FromServices] OrderRepository repo, OrderStatus status)
    {
        var response = await repo.GetOrdersByOrderStatusAsync(status);
        if (!response.IsSuccess)
        {
            return Results.NotFound(response);
        }
        return Results.Ok(response);
    }

    private static async Task<IResult> CreateOrder([FromServices] OrderRepository repo, [FromBody] Order order)
    {
        var response = await repo.CreateOrderAsync(order);
        if (!response.IsSuccess)
        {
            return Results.BadRequest(response);
        }
        return Results.Created($"/api/orders/{response.Data.Id}", response);
    }

    private static async Task<IResult> UpdateOrderStatus([FromServices] OrderRepository repo, Guid id, OrderStatus status)
    {
        var response = await repo.UpdateOrderStatusAsync(id, status);
        if (!response.IsSuccess)
        {
            return Results.NotFound(response);
        }
        return Results.Ok(response);
    }

    private static async Task<IResult> DeleteOrder([FromServices] OrderRepository repo, Guid id)
    {
        var response = await repo.DeleteOrderAsync(id);
        if (!response.IsSuccess)
        {
            return Results.NotFound(response);
        }
        return Results.Ok(response);
    }
}

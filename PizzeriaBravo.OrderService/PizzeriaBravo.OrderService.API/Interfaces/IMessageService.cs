using PizzeriaBravo.OrderService.API.Dto;

namespace PizzeriaBravo.OrderService.API.Interfaces;

public interface IMessageService
{
    Task PublishMessageAsync<T>(MessageDto<T> message);
}

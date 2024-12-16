namespace PizzeriaBravo.OrderService.API.Dto;

public class MessageDto<T>
{
    public string MethodInfo { get; set; }
    public T Data { get; set; }
}

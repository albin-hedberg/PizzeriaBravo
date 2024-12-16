namespace PizzeriaBravo.OrderService.API.Dto;

public class MessageDto<T>
{
    public string MethodInfo { get; set; } // "post", "put", "delete"
    public T Data { get; set; }
}

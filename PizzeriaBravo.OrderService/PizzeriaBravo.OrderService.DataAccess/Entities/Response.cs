namespace PizzeriaBravo.OrderService.DataAccess.Entities;

public class Response<T>
{
    public T? Data { get; set; }
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;
}

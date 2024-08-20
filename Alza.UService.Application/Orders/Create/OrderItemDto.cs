namespace Alza.UService.Application.Orders.Create;

public class OrderItemDto
{
    public required string ProductName { get; init; }

    public required int Quantity { get; init; }

    public required decimal UnitPrice { get; init; }
}

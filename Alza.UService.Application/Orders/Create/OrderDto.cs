namespace Alza.UService.Application.Orders.Create;

public class OrderDto
{
    public required int Number { get; init; }

    public required string CustomerName { get; init; }

    public required IList<OrderItemDto> Items { get; init; }
}

using Alza.UService.Domain.Orders;

namespace Alza.UService.Application.Orders.List;

public class OrderDto
{
    public required int Number { get; init; }

    public required string CustomerName { get; init; }

    public required DateTime CreatedAt { get; init; }

    public required string Status { get; init; }

    public required IList<OrderItemDto> Items { get; init; }
}

using Alza.UService.Domain.Orders;

namespace Alza.UService.Application.Orders;

public class OrderDto
{
    public required int Number { get; init; }

    public required string CustomerName { get; init; }

    public required DateTime CreatedAt { get; init; }

    public required OrderStatus Status { get; init; }

    public required IList<OrderItemDto> Items { get; init; }
}

using Alza.UService.Domain.Common;
using Alza.UService.Domain.Orders;

namespace Alza.UService.Infrastructure.DataAccess.Entities.Mappings;

internal static class OrderMapper
{
    internal static Order ToDomainEntity(this DboOrder order)
    {
        var orderItems = order.OrderItems.Select(x => OrderItem.Create(
            Text.Create(x.ProductName).Value, x.Quantity, Price.Create(x.UnitPrice).Value).Value).ToArray();
        return Order.Create(order.OrderNumber,
            Text.Create(order.CustomerName).Value, order.CreatedAt, orderItems).Value;
    }
}

using Alza.UService.Domain.Common;
using Alza.UService.Domain.Orders;

namespace Alza.UService.Infrastructure.DataAccess.Entities.Mappings;

internal static class OrderMapper
{
    internal static Order ToDomainEntity(this DboOrder order)
    {
        var orderItems = order.OrderItems.Select(x => OrderItem.Create(
                Text.Create(x.ProductName).Value,
                x.Quantity,
                Price.Create(x.UnitPrice).Value).Value)
            .ToArray();
        return Order.Create(
            order.Id,
            order.OrderNumber,
            Text.Create(order.CustomerName).Value,
            order.CreatedAt,
            orderItems).Value;
    }

    internal static void ToEntity(this Order order, ref DboOrder dboOrder)
    {
        dboOrder.CustomerName = order.CustomerName.Value;
        dboOrder.OrderNumber = order.Id.Value;
        dboOrder.OrderStatus = order.Status.ToString();
        dboOrder.CreatedAt = order.CreatedAt;

        var orderItemEntities = new List<DboOrderItem>();
        foreach (var orderItem in order.OrderItems)
        {
            var orderItemEntity = new DboOrderItem()
            {
                Order = dboOrder,
                ProductName = orderItem.ProductName.Value,
                Quantity = orderItem.Quantity,
                UnitPrice = orderItem.UnitPrice.Value,
            };

            orderItemEntities.Add(orderItemEntity);
        }

        dboOrder.OrderItems = orderItemEntities;
    }
}

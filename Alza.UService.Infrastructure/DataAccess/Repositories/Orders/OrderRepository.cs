using System;
using Alza.UService.Application.Orders;
using Alza.UService.Domain.Orders;
using Alza.UService.Infrastructure.DataAccess.Entities;
using Alza.UService.Infrastructure.DataAccess.Entities.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Alza.UService.Infrastructure.DataAccess.Repositories.Orders;

internal class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _dbContext;

    public OrderRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Order?> FindById(OrderId id, CancellationToken cancellationToken = default)
    {
        var order = await _dbContext.Set<DboOrder>()
            .Where(x => x.OrderNumber == id)
            .Select(x => x.ToDomainEntity())
            .SingleOrDefaultAsync(cancellationToken);
        return order;
    }

    public async Task<Guid> Save(Order order, CancellationToken cancellationToken = default)
    {
        var orderEntity = new DboOrder()
        {
            CustomerName = order.CustomerName.Value,
            OrderNumber = order.OrderId.Value,
            OrderStatus = order.Status.ToString(),
            CreatedAt = order.CreatedAt,
        };

        var orderItemEntities = new List<DboOrderItem>();
        foreach (var orderItem in order.OrderItems)
        {
            var orderItemEntity = new DboOrderItem()
            {
                Order = orderEntity,
                ProductName = orderItem.ProductName.Value,
                Quantity = orderItem.Quantity,
                UnitPrice = orderItem.UnitPrice.Value,
            };

            orderItemEntities.Add(orderItemEntity);
        }

        await _dbContext.AddRangeAsync(orderItemEntities, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return orderEntity.Id;
    }
}

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
        var updateEntity = order.OrderDbId != default;
        DboOrder? orderEntity;

        if (updateEntity)
        {
            orderEntity = await _dbContext.Set<DboOrder>().Include(x => x.OrderItems)
                .Where(x => x.Id == order.OrderDbId).FirstOrDefaultAsync(cancellationToken);

            if (orderEntity == null)
            {
                throw new InvalidOperationException($"Entity not found: {order.OrderDbId}");
            }

            order.ToEntity(ref orderEntity);
        }
        else
        {
            orderEntity = new DboOrder();
            _dbContext.Add(orderEntity);

            order.ToEntity(ref orderEntity);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        return orderEntity.Id;
    }

}

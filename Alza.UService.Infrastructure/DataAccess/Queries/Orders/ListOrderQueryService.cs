using Alza.UService.Application.Orders.List;
using Alza.UService.Domain.Orders;
using Alza.UService.Infrastructure.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Alza.UService.Infrastructure.DataAccess.Queries.Orders;

internal class ListOrderQueryService : IListOrderQueryService
{
    private readonly AppDbContext _dbContext;

    public ListOrderQueryService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<OrderDto>> List(CancellationToken cancellationToken = default)
    {
        var result = await _dbContext.Set<DboOrder>()
          .Select(o => new OrderDto()
          {
              Number = o.OrderNumber,
              Status = o.OrderStatus,
              CreatedAt = o.CreatedAt.Date,
              CustomerName = o.CustomerName,
              Items = o.OrderItems.Select(oi => new OrderItemDto()
              {
                  ProductName = oi.ProductName,
                  Quantity = oi.Quantity,
                  UnitPrice = oi.UnitPrice,
              }).ToList(),
          })
          .ToListAsync(cancellationToken);

        return result;
    }
}

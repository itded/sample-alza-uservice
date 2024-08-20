using Alza.UService.Application.Orders;
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

    public async Task<IReadOnlyCollection<OrderDto>> List()
    {
        var result = await _dbContext.Set<DboOrder>()
          .Select(o => new OrderDto()
          {
              Number = o.OrderNumber,
              Status = Enum.Parse<OrderStatus>(o.OrderStatus),
              CreatedAt = o.CreatedAt.Date,
              CustomerName = o.CustomerName,
              Items = o.OrderItems.Select(oi => new OrderItemDto()
              {
                  ProductName = oi.ProductName,
                  Quantity = oi.Quantity,
                  UnitPrice = oi.UnitPrice,
              }).ToList(),
          })
          .ToListAsync();

        return result;
    }
}

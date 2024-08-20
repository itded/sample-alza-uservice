using Alza.UService.Domain;
using Alza.UService.Domain.Orders;

namespace Alza.UService.Application.Orders;

public interface IOrderRepository : IRepository<Order, OrderId>
{
    Task<Guid> Save(Order order, CancellationToken cancellationToken = default);
}

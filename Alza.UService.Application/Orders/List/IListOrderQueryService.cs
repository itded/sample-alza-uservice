namespace Alza.UService.Application.Orders.List;

public interface IListOrderQueryService
{
    Task<IReadOnlyCollection<OrderDto>> List(CancellationToken cancellationToken = default);
}

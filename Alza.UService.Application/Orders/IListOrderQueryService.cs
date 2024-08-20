namespace Alza.UService.Application.Orders;

public interface IListOrderQueryService
{
    Task<IReadOnlyCollection<OrderDto>> List();
}

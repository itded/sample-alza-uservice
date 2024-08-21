using CSharpFunctionalExtensions;
using MediatR;

namespace Alza.UService.Application.Orders.Update;

internal class OrderPaymentCommandHandler : IRequestHandler<OrderPaymentCommand, Result>
{
    private readonly IOrderRepository _orderRepository;

    public OrderPaymentCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Result> Handle(OrderPaymentCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.FindById(request.OrderId, cancellationToken);
        if (order is null)
        {
            return Result.Failure($"The order not found: '{request.OrderId.Value}'");
        }

        if (request.OrderPayment == OrderPaymentEnum.Paid)
        {
            order.MarkAsPaid();
        }
        else
        {
            order.Cancel();
        }

        await _orderRepository.Save(order, cancellationToken);
        return Result.Success();
    }
}
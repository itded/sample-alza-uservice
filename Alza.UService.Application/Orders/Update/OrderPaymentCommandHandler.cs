using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Alza.UService.Application.Orders.Update;

internal class OrderPaymentCommandHandler : IRequestHandler<OrderPaymentCommand, Result>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<OrderPaymentCommandHandler> _logger;

    public OrderPaymentCommandHandler(IOrderRepository orderRepository, ILogger<OrderPaymentCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task<Result> Handle(OrderPaymentCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.FindById(request.OrderId, cancellationToken);
        if (order is null)
        {
            _logger.LogInformation($"The order not found: '{request.OrderId.Value}'");
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
        _logger.LogInformation($"The order updated: '{request.OrderId.Value}', '{request.OrderPayment.ToString()}'");
        return Result.Success();
    }
}
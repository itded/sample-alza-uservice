using Alza.UService.Domain.Common;
using Alza.UService.Domain.Orders;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Alza.UService.Application.Orders.Create;

internal class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<Guid>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<CreateOrderCommandHandler> _logger;

    public CreateOrderCommandHandler(IOrderRepository orderRepository, ILogger<CreateOrderCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task<Result<Guid>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.FindById(request.Order.Number, cancellationToken);
        if (order is not null)
        {
            _logger.LogInformation($"The order already exists: '{request.Order.Number}'");
            return Result.Failure<Guid>($"The order already exists: '{request.Order.Number}'");
        }

        var requestOrder = request.Order;
        var orderItems = requestOrder.Items.Select(x => OrderItem.Create(
            Text.Create(x.ProductName).Value,
            x.Quantity,
            Price.Create(x.UnitPrice).Value).Value)
            .ToArray();
        var newOrder = Order.Create(requestOrder.Number,
            Text.Create(requestOrder.CustomerName).Value,
            DateTimeOffset.Now,
            orderItems).Value;
        var result = await _orderRepository.Save(newOrder);
        return result;
    }
}

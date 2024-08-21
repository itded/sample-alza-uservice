using Alza.UService.Application.Orders.Update;
using Alza.UService.Infrastructure.Scheduling.Payments;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Alza.UService.Infrastructure.Scheduling;

internal class SchedulingService : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IPaymentQueueService _paymentQueueService;
    private readonly ILogger<SchedulingService> _logger;

    public SchedulingService(IServiceScopeFactory serviceScopeFactory, IPaymentQueueService paymentQueueService, ILogger<SchedulingService> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _paymentQueueService = paymentQueueService;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Scheduler started");
        while (!cancellationToken.IsCancellationRequested)
        {
            var paymentItem = await _paymentQueueService.Dequeue(cancellationToken);
            try
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                await mediator.Send(new OrderPaymentCommand(paymentItem.Number, paymentItem.OrderPayment));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}

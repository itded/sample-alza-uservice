namespace Alza.UService.Infrastructure.Scheduling.Payments;

/// <summary>
/// An interface for the payment queue service
/// </summary>
public interface IPaymentQueueService
{
    Task Enqueue(PaymentItem request);

    ValueTask<PaymentItem> Dequeue(CancellationToken cancellationToken);
}

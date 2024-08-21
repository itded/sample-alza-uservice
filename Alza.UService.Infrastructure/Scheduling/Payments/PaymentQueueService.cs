using System.Threading.Channels;

namespace Alza.UService.Infrastructure.Scheduling.Payments;

internal class PaymentQueueService : IPaymentQueueService
{
    private readonly Channel<PaymentItem> _channel;

    public PaymentQueueService()
    {
        _channel = Channel.CreateBounded<PaymentItem>(new BoundedChannelOptions(100)
        {
            FullMode = BoundedChannelFullMode.Wait,
            Capacity = 100,
            SingleReader = true,
        });
    }

    public async Task Enqueue(PaymentItem message)
    {
        await _channel.Writer.WriteAsync(message);
    }

    public async ValueTask<PaymentItem> Dequeue(CancellationToken cancellationToken)
    {
        return await _channel.Reader.ReadAsync(cancellationToken);
    }
}

using Alza.UService.Application.Orders.Update;

namespace Alza.UService.Infrastructure.Scheduling.Payments;

/// <summary>
/// An item in the order payment queue
/// </summary>
public class PaymentItem
{
    public int Number { get; set; }

    public OrderPaymentEnum OrderPayment { get; set; }
}

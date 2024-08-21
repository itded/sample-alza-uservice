namespace Alza.UService.Application.Orders.Update;

public class OrderPaymentDto
{
    public required int Number { get; init; }

    public required bool IsPaid { get; init; }
}

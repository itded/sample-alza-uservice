namespace Alza.UService.Domain.Orders;

/// <summary>
/// Strongly typed Order Id
/// </summary>
/// <param name="Id"></param>
public readonly record struct OrderId(int Value) : IComparable<OrderId>
{
    public int CompareTo(OrderId other)
    {
        return Value.CompareTo(other.Value);
    }

    public static implicit operator OrderId(int id) => new OrderId(id);
    public static explicit operator int(OrderId id) => id.Value;
}

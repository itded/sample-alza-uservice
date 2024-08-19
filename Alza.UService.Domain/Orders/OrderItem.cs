using Alza.UService.Domain.Common;

namespace Alza.UService.Domain.Orders;

/// <summary>
/// Order item specifies the quantity of a product and its unit price
/// </summary>
public class OrderItem : ValueObject
{
    public Text ProductName { get; }

    public int Quantity { get; }

    public Price UnitPrice { get; }

    public static Result<OrderItem> Create(Text productName, int quantity, Price unitPrice)
    {
        if (quantity <= 0)
        {
            return Result.Failure<OrderItem>($"The quantity value cannot be negative: {quantity}");
        }

        return new OrderItem(productName, quantity, unitPrice);
    }

    private OrderItem(Text productName, int quantity, Price unitPrice)
    {
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return ProductName;
        yield return Quantity;
        yield return UnitPrice;
    }
}

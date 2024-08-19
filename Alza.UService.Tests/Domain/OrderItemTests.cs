using Alza.UService.Domain.Common;
using Alza.UService.Domain.Orders;

namespace Alza.UService.Tests.Domain;

public class OrderItemTests
{
    [Theory]
    [MemberData(nameof(ValidData))]
    public void Order_item_is_valid(string productName, int quantity, decimal unitPrice)
    {
        var result = OrderItem.Create(
            Text.Create(productName).Value,
            quantity,
            Price.Create(unitPrice).Value);
        Assert.True(result.IsSuccess);
        Assert.Equal(result.Value.ProductName.Value, productName);
        Assert.Equal(result.Value.Quantity, quantity);
        Assert.Equal(result.Value.UnitPrice.Value, unitPrice);
    }

    [Theory]
    [MemberData(nameof(InvalidData))]
    public void Cannot_create_invalid_order_item(string productName, int quantity, decimal unitPrice)
    {
        Assert.Throws<ResultFailureException>(() => OrderItem.Create(
            Text.Create(productName).Value,
            quantity,
            Price.Create(unitPrice).Value).Value);
    }

    public static TheoryData<string, int, decimal> ValidData =>
        new()
        {
            { "product 1", 3, 299.99m },
            { "free product 1", 1, 0 }
        };

    public static TheoryData<string, int, decimal> InvalidData =>
        new()
        {
            { "product 1", 0, 299.99m },
            { "free product 1", -5, 0 },
            { " ", 1, 299.99m },
            { "free product 1", 1, -1 }
        };
}

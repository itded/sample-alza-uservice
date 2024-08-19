using Alza.UService.Domain.Common;
using Alza.UService.Domain.Orders;

namespace Alza.UService.Tests.Domain;

public class OrderTests
{
    [Fact]
    public void Order_is_cancelled() {
        var orderItems = new[]
        {
            OrderItem.Create(
                Text.Create("product 1").Value,
                5,
                Price.Create(20).Value).Value,
            OrderItem.Create(
                Text.Create("product 2").Value,
                3,
                Price.Create(9.99m).Value).Value,
        };

        var order = Order.Create(
            Text.Create("Customer Name").Value,
            DateTimeOffset.Now,
            orderItems).Value;

        Assert.True(order.IsPending);
        Assert.True(order.Cancel().IsSuccess);
        Assert.True(order.IsCancelled);
        Assert.True(order.Cancel().IsFailure);
        Assert.True(order.MarkAsPaid().IsFailure);
    }

    [Fact]
    public void Order_is_complete()
    {
        var orderItems = new[]
        {
            OrderItem.Create(
                Text.Create("product 1").Value,
                5,
                Price.Create(20).Value).Value,
            OrderItem.Create(
                Text.Create("product 2").Value,
                3,
                Price.Create(9.99m).Value).Value,
        };

        var order = Order.Create(
            Text.Create("Customer Name").Value,
            DateTimeOffset.Now,
            orderItems).Value;
        Assert.True(order.IsPending);
        Assert.True(order.MarkAsPaid().IsSuccess);
        Assert.True(order.IsComplete);
        Assert.True(order.Cancel().IsFailure);
        Assert.True(order.MarkAsPaid().IsFailure);
    }
}

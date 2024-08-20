using Alza.UService.Application.Orders;
using Alza.UService.Domain.Common;
using Alza.UService.Domain.Orders;
using Microsoft.Extensions.DependencyInjection;

namespace Alza.UService.Tests.Infrastructure;

public class OrderRepositoryTests : BaseInfrastructureTests
{
    public OrderRepositoryTests(TestFixture testFixture) : base(testFixture)
    {
    }

    [Fact]
    public async Task Get_order_by_order_id()
    {
        await CleanData();

        // arrange
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
            new OrderId(1),
            Text.Create("Customer Name").Value,
            DateTimeOffset.Now,
            orderItems).Value;

        Order? savedOrder = null;
        await RunScoped(async scope =>
        {
            var orderRepository = scope.ServiceProvider.GetRequiredService<IOrderRepository>();
            await orderRepository.Save(order);

            var nullOrder = await orderRepository.FindById(5);

            savedOrder = await orderRepository.FindById(order.OrderId);
        });

        Assert.NotNull(savedOrder);
        Assert.NotEmpty(savedOrder.OrderItems);

        Assert.Equal(order.Id, savedOrder.Id);
        Assert.Equal(order.Status, savedOrder.Status);
        Assert.Equal(order.CustomerName, savedOrder.CustomerName);

        var savedOrderItem1 = savedOrder.OrderItems.FirstOrDefault(x => x.ProductName.Value == "product 1");
        Assert.NotNull(savedOrderItem1);

        Assert.Equal(20, savedOrderItem1.UnitPrice.Value);
        Assert.Equal(5, savedOrderItem1.Quantity);
    }
}

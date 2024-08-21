using Alza.UService.Application.Orders.Create;
using Alza.UService.Application.Orders.List;
using Alza.UService.Application.Orders.Update;
using Alza.UService.Domain.Orders;
using Microsoft.Extensions.DependencyInjection;
using OrderDto = Alza.UService.Application.Orders.Create.OrderDto;

namespace Alza.UService.Tests.Application;

public class OrderPaymentCommandTests : BaseApplicationTests
{
    public OrderPaymentCommandTests(TestFixture testFixture) : base(testFixture)
    {
    }

    [Fact]
    public async Task Can_pay_order()
    {
        var orderDto = new OrderDto()
        {
            CustomerName = "Customer 1",
            Number = 1,
            Items =
            [
                new()
                {
                    ProductName = "Product 1",
                    UnitPrice = 10.99m,
                    Quantity = 1,
                },
            ]
        };

        await RunScoped(async scope =>
        {
            var createCommand = new CreateOrderCommand(orderDto);
            var createResult = await SendCommand(scope, createCommand);
            Assert.True(createResult.IsSuccess);

            var paymentCommand = new OrderPaymentCommand(orderDto.Number, OrderPaymentEnum.Paid);
            var paymentResult = await SendCommand(scope, paymentCommand);
            Assert.True(paymentResult.IsSuccess);

            var orderQueryService = scope.ServiceProvider.GetRequiredService<IListOrderQueryService>();
            var listResult = await orderQueryService.List();
            Assert.Single(listResult);

            var paidOrder = listResult.First();
            Assert.Equal("Customer 1", paidOrder.CustomerName);
            Assert.Equal(1, paidOrder.Number);
            Assert.Equal(OrderStatus.Complete, paidOrder.Status);

            Assert.Single(paidOrder.Items);
            var paidOrderItem = paidOrder.Items.First();
            Assert.Equal("Product 1", paidOrderItem.ProductName);
            Assert.Equal(1, paidOrderItem.Quantity);
            Assert.Equal(10.99m, paidOrderItem.UnitPrice);
        });
    }
}

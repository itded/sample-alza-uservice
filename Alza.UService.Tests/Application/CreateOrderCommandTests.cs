using Alza.UService.Application.Orders.Create;

namespace Alza.UService.Tests.Application;

public class CreateOrderCommandTests : BaseApplicationTests
{
    public CreateOrderCommandTests(TestFixture testFixture) : base(testFixture)
    {
    }

    [Fact]
    public async Task Can_create_order()
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
                    UnitPrice = 10,
                    Quantity = 1,
                },
            ]
        };

        var command = new CreateOrderCommand(orderDto);
        var result = await SendCommand(command);
        
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task Cannot_create_order_if_exists()
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
                    UnitPrice = 10,
                    Quantity = 1,
                },
            ]
        };

        await RunScoped(async scope =>
        {
            var successCommand = new CreateOrderCommand(orderDto);
            var successResult = await SendCommand(scope, successCommand);
            Assert.True(successResult.IsSuccess);

            var failureCommand = new CreateOrderCommand(orderDto);
            var failureResult = await SendCommand(scope, failureCommand);
            Assert.True(failureResult.IsFailure);
        });
    }
}

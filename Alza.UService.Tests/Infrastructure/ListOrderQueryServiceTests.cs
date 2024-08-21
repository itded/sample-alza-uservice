using Alza.UService.Application.Orders.List;
using Alza.UService.Infrastructure.DataAccess;
using Alza.UService.Infrastructure.DataAccess.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Alza.UService.Tests.Infrastructure;

public class ListOrderQueryServiceTests : BaseInfrastructureTests
{
    public ListOrderQueryServiceTests(TestFixture testFixture): base(testFixture)
    {
    }

    [Fact]
    public async Task Service_returns_empty_list()
    {
        await RunScoped(async scope =>
        {
            var orderQueryService = scope.ServiceProvider.GetRequiredService<IListOrderQueryService>();
            var result = await orderQueryService.List();

            Assert.Empty(result);
        });
    }

    [Fact]
    public async Task Service_returns_list_contains_single_item()
    {
        await RunScoped(async scope =>
        {
            // arrange
            var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var order = new DboOrder()
            {
                CustomerName = "Test Customer Name",
                OrderNumber = 1,
                OrderStatus = "Pending",
            };

            var orderItem = new DboOrderItem()
            {
                Order = order,
                ProductName = "Test Product Name",
                Quantity = 1,
                UnitPrice = 4.99m,
            };

            await appDbContext.AddAsync(orderItem);
            await appDbContext.SaveChangesAsync();

            // act
            var orderQueryService = scope.ServiceProvider.GetRequiredService<IListOrderQueryService>();
            var result = await orderQueryService.List();

            // assert
            Assert.Single(result);

            var orderDto = result.First();
            Assert.Equal(order.CustomerName, orderDto.CustomerName);
            Assert.Equal(order.OrderNumber, orderDto.Number);
            Assert.Equal(order.OrderStatus, orderDto.Status);

            Assert.Single(orderDto.Items);
            var orderItemDto = orderDto.Items.First();
            Assert.Equal(orderItem.ProductName, orderItemDto.ProductName);
            Assert.Equal(orderItem.Quantity, orderItemDto.Quantity);
            Assert.Equal(orderItem.UnitPrice, orderItemDto.UnitPrice);
        });
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(10)]
    public async Task Service_returns_list(int orderCount)
    {
        await RunScoped(async scope =>
        {
            // arrange
            var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var orderItemCount = await BuildData(appDbContext, orderCount);

            // act
            var orderQueryService = scope.ServiceProvider.GetRequiredService<IListOrderQueryService>();
            var result = await orderQueryService.List();

            // assert
            Assert.Equal(orderCount, result.Count());

            var resultOrderItemCount = result.Sum(x => x.Items.Count);
            Assert.Equal(orderItemCount, resultOrderItemCount);
        });
    }

    private async Task<int> BuildData(AppDbContext appDbContext, int count)
    {
        var orderItems = new List<DboOrderItem>();
        for (var i = 1; i <= count; i++)
        {
            var order = new DboOrder()
            {
                CustomerName = $"Customer {i}",
                OrderNumber = i,
                OrderStatus = "Pending",
            };

            var orderItemCount = i % 3 + 1;
            for (var j = 1; j <= orderItemCount; j++)
            {
                var orderItem = new DboOrderItem()
                {
                    Order = order,
                    ProductName = $"Product {i}-{j}",
                    Quantity = i * 5,
                    UnitPrice = 2_000 * j * (i / 3),
                };
                orderItems.Add(orderItem);
            }
        }

        await appDbContext.AddRangeAsync(orderItems);
        await appDbContext.SaveChangesAsync();

        return orderItems.Count;
    }
}

//using Alza.UService.Application.Orders;
//using Alza.UService.Tests.Infrastructure;
//using Microsoft.Extensions.DependencyInjection;

//namespace Alza.UService.Tests.Application;

//public class CreateOrderCommandTests : BaseInfrastructureTests
//{
//    public async Task Can_create_order()
//    {
//        await RunScoped(async scope =>
//        {
//            var repository = scope.ServiceProvider.GetRequiredService<IOrderRepository>();
//            repository.Save()
//        });
//    }
//}

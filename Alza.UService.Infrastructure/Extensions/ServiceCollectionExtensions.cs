using Alza.UService.Application.Orders;
using Alza.UService.Application.Orders.List;
using Alza.UService.Infrastructure.DataAccess;
using Alza.UService.Infrastructure.DataAccess.Queries.Orders;
using Alza.UService.Infrastructure.DataAccess.Repositories.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Alza.UService.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IListOrderQueryService, ListOrderQueryService>();
        services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()));
        return services;
    }

    /// <summary>
    /// Removes the single occurence of a service descriptor for the specified service type.
    /// </summary>
    public static void RemoveDescriptor(this IServiceCollection services, Type serviceType)
    {
        var descriptor = services.SingleOrDefault(s => s.ServiceType == serviceType);
        if (descriptor != null)
        {
            services.Remove(descriptor);
        }
    }
}

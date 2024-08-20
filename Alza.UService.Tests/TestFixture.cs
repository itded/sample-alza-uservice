using Microsoft.Extensions.DependencyInjection;
using Alza.UService.Infrastructure.Extensions;
using Alza.UService.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Alza.UService.Tests;

public class TestFixture
{
    public TestFixture()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddApplicationServices();
        serviceCollection.AddInfrastructureServices();

        // Lets pretend that the infrastructure module has different DbContext setup
        serviceCollection.RemoveDescriptor(typeof(AppDbContext));
        serviceCollection.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()));

        ServiceProvider = serviceCollection.BuildServiceProvider();
    }

    public ServiceProvider ServiceProvider { get; }
}

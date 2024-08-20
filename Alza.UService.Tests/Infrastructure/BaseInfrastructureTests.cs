using Alza.UService.Infrastructure.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace Alza.UService.Tests.Infrastructure;

public abstract class BaseInfrastructureTests : IClassFixture<TestFixture>
{
    private readonly TestFixture _testFixture;

    public BaseInfrastructureTests(TestFixture testFixture)
    {
        _testFixture = testFixture;
    }

    /// <summary>
    /// A simple way to clean DB data.
    /// Please check <see href="https://github.com/jbogard/Respawn"/> to create more intelligent cleaner for the relation database.
    protected async Task CleanData()
    {
        await RunScoped(async scope =>
        {
            var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await appDbContext.Database.EnsureDeletedAsync();
            await appDbContext.Database.EnsureCreatedAsync();
        });
    }

    protected async Task RunScoped(Func<IServiceScope, Task> action)
    {
        using var scope = _testFixture.ServiceProvider.CreateScope();
        await action.Invoke(scope);
    }
}

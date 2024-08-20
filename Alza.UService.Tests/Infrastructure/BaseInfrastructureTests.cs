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

    protected async Task RunScoped(Func<IServiceScope, Task> action)
    {
        using var scope = _testFixture.ServiceProvider.CreateScope();
        await action.Invoke(scope);
    }
}

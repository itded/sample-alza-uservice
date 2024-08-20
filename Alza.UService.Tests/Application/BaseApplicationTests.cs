using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Alza.UService.Tests.Application;

public abstract class BaseApplicationTests : IClassFixture<TestFixture>
{
    private readonly TestFixture _testFixture;

    public BaseApplicationTests(TestFixture testFixture)
    {
        _testFixture = testFixture;
    }

    protected async Task RunScoped(Func<IServiceScope, Task> action)
    {
        using var scope = _testFixture.ServiceProvider.CreateScope();
        await action.Invoke(scope);
    }

    protected async Task<TResult> SendCommand<TResult>(IServiceScope scope, IRequest<TResult> command)
    {
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        return await mediator.Send(command);
    }

    protected async Task<TResult> SendCommand<TResult>(IRequest<TResult> command)
    {
        using var scope = _testFixture.ServiceProvider.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        return await mediator.Send(command);
    }
}

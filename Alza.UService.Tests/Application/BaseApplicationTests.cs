using Alza.UService.Tests.Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Alza.UService.Tests.Application;

/// <remarks>
/// TODO: Create a common test class, because the dependency should be inverted
/// </remarks>
public abstract class BaseApplicationTests : BaseInfrastructureTests
{
    private readonly TestFixture _testFixture;

    public BaseApplicationTests(TestFixture testFixture):base(testFixture)
    {
        _testFixture = testFixture;
    }

    protected async Task SendCommand<TResult>(IRequest<TResult> command)
    {
        using var scope = _testFixture.ServiceProvider.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        await mediator.Send(command);
    }
}

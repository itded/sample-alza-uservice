using Alza.UService.Application.Orders.Create;
using Alza.UService.Application.Orders.List;
using Alza.UService.Application.Orders.Update;
using Alza.UService.Backend;
using Alza.UService.Infrastructure.Extensions;
using Alza.UService.Infrastructure.Scheduling.Payments;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CreateOrderDto = Alza.UService.Application.Orders.Create.OrderDto;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddLogging(x =>
{
    x.ClearProviders();
    x.AddConsole();
    x.SetMinimumLevel(LogLevel.Debug);
});

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddScheduling();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.WebHost.UseKestrel(options => options.AddServerHeader = false);

var app = builder.Build();

var isDevelopment = app.Environment.IsDevelopment();
if (isDevelopment)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandler>();

app.MapPost("/order", async Task<IResult> ([FromServices] IMediator mediator, CreateOrderDto order) =>
{
    var result = await mediator.Send(new CreateOrderCommand(order));
    return result.IsFailure ? TypedResults.BadRequest(result.Error) : TypedResults.Ok(result.Value);
}).Produces(400).Produces<Guid>();

app.MapGet("/orders", async ([FromServices] IListOrderQueryService listOrderQueryService) =>
{
    var orders = await listOrderQueryService.List();
    return orders;
});

app.MapPost("/order/payment", async Task<IResult> ([FromServices] IPaymentQueueService paymentQueueService, OrderPaymentDto orderPayment) =>
{
    var paymentItem = new PaymentItem()
    {
        Number = orderPayment.Number,
        OrderPayment = orderPayment.IsPaid ? OrderPaymentEnum.Paid : OrderPaymentEnum.Cancelled
    };
    await paymentQueueService.Enqueue(paymentItem);
    return TypedResults.Ok();
});

await app.RunAsync();

using CSharpFunctionalExtensions;
using MediatR;

namespace Alza.UService.Application.Orders.Create;

/// <summary>
/// Creates a new order
/// </summary>
public record CreateOrderCommand(OrderDto Order) : IRequest<Result<Guid>>;

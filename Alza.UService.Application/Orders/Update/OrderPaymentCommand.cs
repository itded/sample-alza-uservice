using Alza.UService.Domain.Orders;
using CSharpFunctionalExtensions;
using MediatR;

namespace Alza.UService.Application.Orders.Update;

/// <summary>
/// Marks a pending order as paid or cancelled
/// </summary>
public record OrderPaymentCommand(OrderId OrderId, OrderPaymentEnum OrderPayment) : IRequest<Result>;

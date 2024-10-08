﻿using Alza.UService.Domain.Common;

namespace Alza.UService.Domain.Orders;

/// <summary>
/// Order entity specifies a collection of <see cref="OrderItem"/> and a status of the order
/// </summary>
public class Order : Entity<OrderId>
{
    /// <summary>
    /// DB reference Id
    /// </summary>
    public Guid OrderDbId { get; }

    public Text CustomerName { get; }
    public DateTimeOffset CreatedAt { get; }
    public OrderItem[] OrderItems { get; }

    public OrderStatus Status => _status;

    public bool IsPending  => _status == OrderStatus.Pending;

    public bool IsCancelled => _status == OrderStatus.Cancelled;

    public bool IsComplete => _status == OrderStatus.Complete;

    private OrderStatus _status = OrderStatus.Pending;

    public static Result<Order> Create(OrderId orderId, Text customerName, DateTimeOffset createdAt, OrderItem[] orderItems)
    {
        return Create(default, orderId, customerName, createdAt, orderItems);
    }

    public static Result<Order> Create(Guid orderDbId, OrderId orderId, Text customerName, DateTimeOffset createdAt, OrderItem[] orderItems)
    {
        if (orderItems is null || orderItems.Length == 0)
        {
            return Result.Failure<Order>($"The order items collection cannot be null or empty");
        }

        return new Order(orderDbId, orderId, customerName, createdAt, orderItems);
    }

    public Result Cancel()
    {
        if (_status != OrderStatus.Pending)
        {
            return Result.Failure<Order>($"The order transition is not valid.");
        }

        _status = OrderStatus.Cancelled;
        return Result.Success();
    }

    public Result MarkAsPaid()
    {
        if (_status != OrderStatus.Pending)
        {
            return Result.Failure<Order>($"The order transition is not valid.");
        }

        _status = OrderStatus.Complete;
        return Result.Success();
    }

    private Order(Guid orderDbId, OrderId orderId, Text customerName, DateTimeOffset createdAt, OrderItem[] orderItems)
    {
        OrderDbId = orderDbId;
        Id = orderId;
        CustomerName = customerName;
        CreatedAt = createdAt;
        OrderItems = orderItems;
    }
}

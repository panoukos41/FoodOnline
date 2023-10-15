﻿using FluentValidation;

namespace FoodOnline.Orders;

public sealed record OrderModel : IEntity, IValid
{
    public required Uuid Id { get; init; }

    public required Uuid StoreId { get; init; }

    public required Uuid UserId { get; init; }

    public OrderState State { get; init; }

    public Address DeliverTo { get; init; } = Address.Empty;

    public static IValidator Validator { get; } = new InlineValidator<OrderModel>
    {
        // todo: Implement Order Validator
    };
}

/// <summary>
/// Describes several states an order can be in.
/// </summary>
public enum OrderState
{
    /// <summary>
    /// The order has been placed by the customer.
    /// </summary>
    Placed,

    /// <summary>
    /// The order has been received by the store.
    /// </summary>
    Received,

    /// <summary>
    /// The store has confirmed the order.
    /// </summary>
    Confirmed,

    /// <summary>
    /// The order is on it's way to the customer.
    /// </summary>
    Delivering,

    /// <summary>
    /// The order has been delivered.
    /// </summary>
    Delivered,

    /// <summary>
    /// The order was rejected by the store.
    /// </summary>
    Rejected,

    /// <summary>
    /// The order was canceled.
    /// </summary>
    Canceled
}
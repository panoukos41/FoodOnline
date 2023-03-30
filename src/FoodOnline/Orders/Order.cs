using FluentValidation;
using FoodOnline.Stores;

namespace FoodOnline.Orders;

public sealed record Order : IEntity
{

    public required Uuid Id { get; init; }

    public required Uuid StoreId { get; init; }

    public required Uuid UserId { get; init; }

    //public OrderState State { get; set; }

    public Address Address { get; set; }

    public decimal TotalPriceEur { get; set; }

    public static IValidator Validator { get; } = new InlineValidator<Order>
    {

    };
}

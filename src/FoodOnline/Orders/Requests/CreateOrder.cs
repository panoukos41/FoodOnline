//namespace FoodOnline.Orders.Requests;

//public class CreateOrder : IRequest<NewOrderModel>
//{
//    /// <summary>
//    /// A token unique for the order created by the client.
//    /// </summary>
//    public Guid IdempotencyToken { get; set; }

//    /// <summary>
//    /// A yaml list of the order entries of type OrderItems.
//    /// </summary>
//    public string Entries { get; set; }

//    /// <summary>
//    /// The total price of the order in EUR.
//    /// </summary>
//    public decimal TotalPriceEur { get; set; }

//    /// <summary>
//    /// The address to which it should be delivered.
//    /// This is a <see cref="ValueObjectBase"/>
//    /// </summary>
//    public Address Address { get; set; }

//    /// <summary>
//    /// The id of the store.
//    /// </summary>
//    public string StoreId { get; set; }

//    /// <summary>
//    /// The id of the user. Can be null.
//    /// </summary>
//    public string UserId { get; set; }
//}

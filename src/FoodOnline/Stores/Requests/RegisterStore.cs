namespace FoodOnline.Stores.Requests;

public class RegisterStore : IRequest<string>
{
    public required Uuid Owner { get; init; }

    public required Store Store { get; set; }
}

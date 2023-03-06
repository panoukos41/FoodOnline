using FoodOnline.Infrastructure.Abstractions.Handlers;
using FoodOnline.Requests.AuthRequests;

namespace FoodOnline.Infrastructure.Handlers.AuthHandlers;

public sealed class LoginUserHandler : CommandHandler<LoginUser, string>
{
    public override string Collection => throw new NotImplementedException();

    public override ValueTask<Result<string>> Handle(LoginUser command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

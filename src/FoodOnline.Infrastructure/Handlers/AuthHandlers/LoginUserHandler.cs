using FoodOnline.Infrastructure.Abstractions.Handlers;
using FoodOnline.Requests.AuthRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOnline.Infrastructure.Handlers.AuthHandlers;

public sealed class LoginUserHandler : CommandHandler<LoginUser, string>
{
    public override string Collection => throw new NotImplementedException();

    public override ValueTask<Result<string>> Handle(LoginUser command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

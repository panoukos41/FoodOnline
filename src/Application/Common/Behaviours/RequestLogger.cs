using FoodOnline.Application.Common.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace FoodOnline.Application.Common.Behaviours
{
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger logger;
        private readonly ICurrentUser currentUser;
        private readonly IIdentityService identityService;

        public RequestLogger(ILogger<TRequest> logger, ICurrentUser currentUser, IIdentityService identityService)
        {
            this.logger = logger;
            this.currentUser = currentUser;
            this.identityService = identityService;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var userId = currentUser.Id ?? string.Empty;
            var userName = string.IsNullOrEmpty(userId)
                ? string.Empty
                : await identityService.GetUsernameAsync(userId);

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(userName))
            {
                logger.LogInformation("Request: {Name} {@UserId} {@UserName} {@Request}", requestName, userId, userName, request);
            }
            else
            {
                logger.LogInformation("Request: {Name} {@Request}", requestName, request);
            }
        }
    }
}
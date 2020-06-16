using AspNet.Security.OpenIdConnect.Primitives;
using FoodOnline.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FoodOnline.WebApi.Services
{
    public class CurrentUserService : ICurrentUser
    {
        public string Id { get; }

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            Id = httpContextAccessor.HttpContext?.User?.FindFirstValue(OpenIdConnectConstants.Claims.Subject);
        }
    }
}
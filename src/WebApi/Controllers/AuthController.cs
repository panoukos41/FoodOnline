using AspNet.Security.OpenIdConnect.Primitives;
using FoodOnline.Application.Auth.Requests;
using FoodOnline.Domain;
using FoodOnline.Domain.Auth.Requests;
using FoodOnline.Domain.StoreUsers.Requests;
using FoodOnline.Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Server;
using OpenIddict.Validation;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FoodOnline.WebApi.Controllers
{
    public class AuthController : ApiController
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterUser register)
        {
            try
            {
                var id = await Send(register);
                await Send(new CreateUser
                {
                    Id = id,
                    Email = register.Email,
                    Name = register.Name,
                    LoginProvider = Providers.FoodOnline,
                    ProviderDisplayName = "Food Online"
                });
                return Ok();
            }
            catch (System.Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpPost("token")]
        public async Task<IActionResult> Token(OpenIdConnectRequest request)
        {
            if (request.IsPasswordGrantType())
            {
                try
                {
                    var @for = request.GetParameter("for").GetValueOrDefault();

                    return @for == "Business"
                        ? await PasswordBusiness(request.Username, request.Password)
                        : await Password(request.Username, request.Password);
                }
                catch
                {
                    return BadRequest(new OpenIdConnectResponse
                    {
                        Error = OpenIdConnectConstants.Errors.InvalidGrant,
                        ErrorDescription = "Invalid email or password"
                    });
                }
            }
            else if (request.IsRefreshTokenGrantType())
            {
                // todo: test!
                // Retrieve the claims principal stored in the refresh token.
                var principal = (await HttpContext.AuthenticateAsync(OpenIddictValidationDefaults.AuthenticationScheme)).Principal;
                return RefreshToken(principal);
            }
            return BadRequest(new OpenIdConnectResponse
            {
                Error = OpenIdConnectConstants.Errors.InvalidClient,
                ErrorDescription = "The grant_type is not supported"
            });
        }

        private async Task<IActionResult> Password(string email, string password)
        {
            var user = await Mediator.Send(new LoginUser { Email = email, Password = password });

            var ticket = new TicketBuilder()
                .AddClaim(OpenIdConnectConstants.Claims.Role, Role.User, "access_token")
                .AddClaim(OpenIdConnectConstants.Claims.Subject, user.Id, "access_token")
                .AddClaim(OpenIdConnectConstants.Claims.Name, user.Name, "id_token")
                .AddClaim(ClaimTypes.Name, user.Name, "id_token")
                .SetScopes(
                    OpenIdConnectConstants.Scopes.OpenId,
                    OpenIdConnectConstants.Scopes.Profile,
                    OpenIdConnectConstants.Scopes.OfflineAccess)
                .GetTicket();

            return SignIn(ticket.Principal, ticket.Properties, ticket.AuthenticationScheme);
        }

        private async Task<IActionResult> PasswordBusiness(string username, string password)
        {
            var (id, role, storeId) = await Mediator.Send(new AuthStoreUser { Username = username, Password = password });
            var builder = new TicketBuilder()
                .AddClaim(OpenIdConnectConstants.Claims.Role, role, "access_token")
                .AddClaim(OpenIdConnectConstants.Claims.Subject, id, "id_token")
                .AddClaim(OpenIdConnectConstants.Claims.Name, username, "id_token")
                .AddClaim(ClaimTypes.Role, role, "id_token")
                .AddClaim(ClaimTypes.Name, username, "id_token")
                .SetScopes(
                    OpenIdConnectConstants.Scopes.OpenId,
                    OpenIdConnectConstants.Scopes.Profile,
                    OpenIdConnectConstants.Scopes.OfflineAccess);

            if (storeId != null)
                builder.AddClaim("store_id", storeId, "id_token");

            var ticket = builder.GetTicket();

            return SignIn(ticket.Principal, ticket.Properties, ticket.AuthenticationScheme);
        }

        private IActionResult RefreshToken(ClaimsPrincipal principal)
        {
            if (!principal.TryGetSubject(out _))
            {
                var properties = new AuthenticationProperties(new Dictionary<string, string>
                {
                    [OpenIdConnectConstants.Properties.Error] = OpenIdConnectConstants.Errors.InvalidGrant,
                    [OpenIdConnectConstants.Properties.ErrorDescription] = "The refresh token is no longer valid."
                });
                return Forbid(properties, OpenIddictServerDefaults.AuthenticationScheme);
            }
            return SignIn(principal, OpenIddictServerDefaults.AuthenticationScheme);
        }

        [HttpPost("logout")]
        public async Task<ActionResult> Logout(OpenIdConnectRequest request)
        {
            return BadRequest();
        }
    }
}
using AspNet.Security.OpenIdConnect.Extensions;
using AspNet.Security.OpenIdConnect.Primitives;
using Microsoft.AspNetCore.Authentication;
using OpenIddict.Server;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace FoodOnline.Infrastructure
{
    public class TicketBuilder
    {
        #region Properites

        private ClaimsIdentity? _identity;
        private ClaimsPrincipal? _principal;
        private AuthenticationTicket? _ticket;

        private string Scheme { get; set; } = OpenIddictServerDefaults.AuthenticationScheme;

        private AuthenticationProperties Properties { get; set; } = new AuthenticationProperties();

        public ClaimsIdentity Identity
        {
            get => _identity ??= new ClaimsIdentity("OpenIddict", OpenIdConnectConstants.Claims.Name, OpenIdConnectConstants.Claims.Role);
            set => _identity = value;
        }

        public ClaimsPrincipal Principal
        {
            get => _principal ??= new ClaimsPrincipal(Identity);
            set => _principal = value;
        }

        public AuthenticationTicket Ticket
        {
            get => _ticket ??= new AuthenticationTicket(Principal, Properties, Scheme);
        }

        #endregion

        public TicketBuilder SetIdentity(ClaimsIdentity identity)
        {
            Identity = identity;
            return this;
        }

        public TicketBuilder SetPrincipal(Func<ClaimsIdentity, ClaimsPrincipal> func)
        {
            Principal = func(Identity);
            return this;
        }

        public TicketBuilder SetScheme(string authenticationScheme)
        {
            Scheme = authenticationScheme;
            return this;
        }

        public TicketBuilder SetProperties(AuthenticationProperties properties)
        {
            Properties = properties;
            return this;
        }

        public TicketBuilder AddClaim(Claim claim)
        {
            Identity.AddClaim(claim);
            return this;
        }

        public TicketBuilder AddClaim(string type, string value)
        {
            Identity.AddClaim(type, value);
            return this;
        }

        public TicketBuilder AddClaim(string type, string value, params string[] destinations)
        {
            Identity.AddClaim(type, value, destinations);
            return this;
        }

        public TicketBuilder AddClaim(string type, string value, IEnumerable<string> destinations)
        {
            Identity.AddClaim(type, value, destinations);
            return this;
        }

        public TicketBuilder AddClaims(IEnumerable<Claim> claims)
        {
            Identity.AddClaims(claims);
            return this;
        }

        public TicketBuilder SetScopes(params string[] scopes)
        {
            Ticket.SetScopes(scopes);
            return this;
        }

        public TicketBuilder SetScopes(IEnumerable<string> scopes)
        {
            Ticket.SetScopes(scopes);
            return this;
        }

        public AuthenticationTicket GetTicket()
        {
            return Ticket;
        }
    }
}
using Flurl.Http;
using FoodOnline.Domain.Auth.Requests;
using FoodOnline.WebClient.Models;
using FoodOnline.WebClient.Services;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Threading.Tasks;
using static Newtonsoft.Json.JsonConvert;
using static System.Console;

namespace FoodOnline.WebClient.Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly IFlurlClient http;
        private readonly ApiAuthenticationStateProvider auth;

        public AuthService(IFlurlClient http, AuthenticationStateProvider auth)
        {
            this.http = http;
            this.auth = (ApiAuthenticationStateProvider)auth;
        }

        public async Task<bool> LoginAsync(LoginUser model)
        {
            WriteLine("Starting Authentication process!");

            var response = await http
                .Request("api", "auth", "token")
                .PostUrlEncodedAsync(new
                {
                    grant_type = "password",
                    @for = "Business",
                    username = model.Email,
                    password = model.Password,
                });

            if (!response.IsSuccessStatusCode)
            {
                WriteLine("Authentication failed!");
                return false;
            }

            var authToken = DeserializeObject<AuthToken>(await response.Content.ReadAsStringAsync());

            await auth.MarkUserAsAuthenticated(authToken);

            WriteLine("Authentication succeeded!");
            return true;
        }

        public Task LogoutAsync()
        {
            //todo call logoute endpoint
            return auth.MarkUserAsLoggedOut();
            //return Task.CompletedTask;
        }

        /// <summary>
        /// Not implemented, user registers happen only by stuff and admin.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task RegisterAsync(RegisterUser model)
        {
            throw new NotImplementedException();
        }
    }
}
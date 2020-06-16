using Blazored.LocalStorage;
using Flurl.Http;
using FoodOnline.WebClient.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace FoodOnline.WebClient.Services
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly string token_key;

        private readonly IFlurlClient flurlClient;
        private readonly ILocalStorageService localStorage;

        public ApiAuthenticationStateProvider(IFlurlClient flurlClient, ILocalStorageService localStorage)
        {
            this.flurlClient = flurlClient;
            this.localStorage = localStorage;
            token_key = "idToken";
        }

        public ApiAuthenticationStateProvider(IFlurlClient flurlClient, ILocalStorageService localStorage, string tokenKey)
        {
            this.flurlClient = flurlClient;
            this.localStorage = localStorage;
            token_key = tokenKey;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await localStorage.GetItemAsync<Token>(token_key);

            if (token == null || string.IsNullOrWhiteSpace(token.AuthToken))
            {
                flurlClient.HttpClient.DefaultRequestHeaders.Authorization = null;
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            var dateNow = DateTime.UtcNow;

            if (dateNow > token.ExpireDateUtc)
            {
                await localStorage.RemoveItemAsync(token_key);
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            // This is valid because we know the token lasts 7 days (604800 seconds)
            else if (dateNow.AddDays(2) >= token.ExpireDateUtc)
            {
                _ = RenewTokenAsync(token);
            }

            flurlClient.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token.AuthToken);

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt2(token.IdToken), "jwt")));
        }

        public async Task MarkUserAsAuthenticated(AuthToken authToken)
        {
            await localStorage.SetItemAsync(token_key, new Token(authToken));

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task MarkUserAsLoggedOut()
        {
            await localStorage.RemoveItemAsync(token_key);

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        private async Task RenewTokenAsync(Token token)
        {
            WriteLine("Trying to renew token!");

            try
            {
                var result = await flurlClient
                .Request("api", "auth", "token")
                .AllowAnyHttpStatus()
                .PostUrlEncodedAsync(new
                {
                    grant_type = "refresh_token",
                    refresh_token = token.RefreshToken
                })
                .ReceiveJson<AuthToken>();

                if (result == null)
                {
                    WriteLine("Failed to renew!");
                    return;
                };

                await localStorage.SetItemAsync(token_key, new Token(result));
            }
            catch
            {
                WriteLine("Failed to renew!");
            }
        }

        private IEnumerable<Claim> ParseClaimsFromJwt2(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var json = Encoding.UTF8.GetString(jsonBytes);
            var values = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

            foreach (var item in values)
            {
                yield return new Claim(item.Key, item.Value.ToString());
            }
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
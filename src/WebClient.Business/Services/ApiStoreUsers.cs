using Flurl.Http;
using FoodOnline.Domain.StoreUsers.Models;
using FoodOnline.Domain.StoreUsers.Queries;
using FoodOnline.Domain.StoreUsers.Requests;
using System.Threading.Tasks;

namespace FoodOnline.WebClient.Business.Services
{
    public static class ApiStoreUsers
    {
        public static Task RegisterStoreUser(this IFlurlClient http, RegisterStoreUser model)
        {
            return http.Request("api", "storeuser", "register")
                .PostUrlEncodedAsync(model);
        }

        public static Task UpdateStoreUser(this IFlurlClient http, UpdateStoreUser model)
        {
            return http.Request("api", "storeuser", "update")
                .PostUrlEncodedAsync(model);
        }

        public static Task DeleteStoreUser(this IFlurlClient http, DeleteStoreUser model)
        {
            return http.Request("api", "storeuser", "delete")
                .PostUrlEncodedAsync(model);
        }

        public static Task<StoreUserModel> GetStoreUser(this IFlurlClient http, GetStoreUser model)
        {
            return http.Request("api", "storeuser", "user")
                .SetQueryParams(model)
                .GetJsonAsync<StoreUserModel>();
        }

        public static Task<StoreUserModel[]> GetStoreUsersForStore(this IFlurlClient http, GetStoreUsersrForStore model)
        {
            return http.Request("api", "storeuser", "usersforstore")
                .SetQueryParams(model)
                .GetJsonAsync<StoreUserModel[]>();
        }

        public static Task<StoreUserModel[]> GetStoreUsers(this IFlurlClient http, GetStoreUsers model)
        {
            return http.Request("api", "storeuser", "users")
                .SetQueryParams(model)
                .GetJsonAsync<StoreUserModel[]>();
        }
    }
}
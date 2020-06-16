using Flurl;
using Flurl.Http;
using FoodOnline.Domain.Stores.Models;
using FoodOnline.Domain.Users.Models;
using FoodOnline.Domain.Users.Queries;
using System;
using System.Threading.Tasks;

namespace FoodOnline.WebClient.Services
{
    public static class Api
    {
        private static Task Refresh()
        {

            throw new NotImplementedException();
        }

        public static Task<StoreModel> GetStore(this IFlurlClient http, string id) =>
            http.Request("api", "store", id)
                .GetJsonAsync<StoreModel>();

        public static Task<StoreListModel[]> GetStores(this IFlurlClient http, object query) =>
            http.Request("api", "store")
                .SetQueryParams(query)
                .GetJsonAsync<StoreListModel[]>();

        public static Task<UserModel> GetUser(this IFlurlClient http, GetUser query) =>
            http.Request("api", "user")
                .GetJsonAsync<UserModel>();
    }
}
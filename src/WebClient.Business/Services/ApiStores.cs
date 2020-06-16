using Flurl.Http;
using FoodOnline.Domain.Stores.Models;
using FoodOnline.Domain.Stores.Requests;
using System.Threading.Tasks;

namespace FoodOnline.WebClient.Business.Services
{
    /// <summary>
    ///
    /// </summary>
    public static class ApiStores
    {
        public static Task RegisterStore(this IFlurlClient http, RegisterStore model)
        {
            return http.Request("api", "store", "register")
                .PostUrlEncodedAsync(model);
        }

        public static Task UpdateStore(this IFlurlClient http, UpdateStore model)
        {
            return http.Request("api", "store", "update")
                .PostUrlEncodedAsync(model);
        }

        public static Task UpdateOpenStore(this IFlurlClient http, UpdateOpenStore model)
        {
            return http.Request("api", "store", "open")
                .PostUrlEncodedAsync(model);
        }

        public static Task DeleteStore(this IFlurlClient http, DeleteStore model)
        {
            return http.Request("api", "store", "delete")
                .PostUrlEncodedAsync(model);
        }

        public static Task<StoreListModel[]> GetOwnerStores(this IFlurlClient http, string id)
        {
            return http.Request("api", "store", "ownerstores", id)
                .GetJsonAsync<StoreListModel[]>();
        }

        public static Task<StoreBusinessModel> GetStore(this IFlurlClient http, string id)
        {
            return http.Request("api", "store", id)
                .GetJsonAsync<StoreBusinessModel>();
        }
    }
}
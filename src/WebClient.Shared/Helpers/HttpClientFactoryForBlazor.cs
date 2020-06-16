using Flurl.Http.Configuration;
using System.Net.Http;

namespace FoodOnline.WebClient.Helpers
{
    public class HttpClientFactoryForBlazor : DefaultHttpClientFactory
    {
        public override HttpClient CreateHttpClient(HttpMessageHandler handler)
        {
            return new HttpClient();
        }

        public override HttpMessageHandler CreateMessageHandler()
        {
            return null;
        }
    }
}
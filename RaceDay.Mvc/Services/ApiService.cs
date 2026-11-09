using System.Net.Http.Json;

namespace RaceDay.Mvc.Services
{
    public class ApiService
    {
        private readonly IHttpClientFactory _factory;

        public ApiService(IHttpClientFactory factory)
        {
            _factory = factory;
        }

        public async Task<T?> GetAsync<T>(string url)
        {
            var client = _factory.CreateClient("RaceDayApi");

            return await client.GetFromJsonAsync<T>(url);
        }

        public async Task<HttpResponseMessage> PostAsync<T>(
            string url,
            T data)
        {
            var client = _factory.CreateClient("RaceDayApi");

            return await client.PostAsJsonAsync(url, data);
        }
    }
}
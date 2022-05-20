using System.Threading.Tasks;
using RestSharp;
using System.Text.Json;

namespace App_Luta.Services
{
    public class WebApiService
    {
        private static string BaseUrl { get; set; }
        public void SetUrlBase(string url)
        {
            BaseUrl = url;
        }

        public async Task<T> Request<T>(object data, string service, Method verbo)
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest(service, verbo);
            request.AddHeader("x-api-key", "29452a07-5ff9-4ad3-b472-c7243f548a33");
            if (data is not null)
                request.AddJsonBody(data);
            var response = await client.ExecuteAsync(request);
            return JsonSerializer.Deserialize<T>(response.Content);
        }
    }
}

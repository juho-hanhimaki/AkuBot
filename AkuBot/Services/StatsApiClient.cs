using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AkuBot
{
    public class StatsApiClient
    {
        private static HttpClient client;

        static StatsApiClient()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://cs.ananassi.com/api/");
        }

        public static async Task<string> GetAsync(string uri)
        {
            var response = await client.GetAsync(uri, HttpCompletionOption.ResponseContentRead);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
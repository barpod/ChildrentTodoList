using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChildrenTodoList.Tests
{
    public static class TestExtensions
    {
        public static async Task<T> DeserializeAsync<T>(this HttpResponseMessage response)
        {
            return JsonSerializer.Deserialize<T>(
                await response.Content.ReadAsStringAsync(), 
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }); 
        }

        public static async Task<T> DeserializeAsync<T>(this Task<HttpResponseMessage> responseTask)
        {
            return await DeserializeAsync<T>(await responseTask); ;
        }

        public static async Task<TOutput> PostAndDeserializeAsync<TOutput>(this HttpClient httpClient, string uri, object model)
        {
            return await httpClient.PostAsync( uri,
                new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json"))
                .DeserializeAsync<TOutput>();
        }
    }
}

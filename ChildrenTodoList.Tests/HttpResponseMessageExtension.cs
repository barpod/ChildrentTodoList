using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChildrenTodoList.Tests
{
    public static class HttpResponseMessageExtension
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
    }
}

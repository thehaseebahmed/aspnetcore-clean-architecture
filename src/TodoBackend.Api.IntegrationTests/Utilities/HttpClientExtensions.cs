using System.Text;
using Newtonsoft.Json;

namespace Starter.Api.IntegrationTests.Utilities;

public static class HttpClientExtensions
{
    public static async Task<T> GetAsync<T>(this HttpClient client, string endpoint)
    {
        var response = await client.GetAsync(endpoint);
        return await Parse<T>(response);
    }

    public static async Task<T> PostJsonAsync<T>(this HttpClient client, string endpoint, object? body)
    {
        var json = JsonConvert.SerializeObject(body ?? new object());
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await client.PostAsync(endpoint, content);

        return await Parse<T>(response);
    }

    private static async Task<T> Parse<T>(HttpResponseMessage message)
    {
        var content = await message.Content.ReadAsStringAsync();
        if (!message.IsSuccessStatusCode)
        {
            throw new HttpException(
                $"StatusCode: {(int)message.StatusCode} Response: {content}",
                message.StatusCode,
                content
            );
        }

        return JsonConvert.DeserializeObject<T>(content);
    }
}
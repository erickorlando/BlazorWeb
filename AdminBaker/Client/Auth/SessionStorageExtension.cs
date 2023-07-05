using System.Text.Json;
using Blazored.SessionStorage;

namespace AdminBaker.Client.Auth;

public static class SessionStorageExtension
{
    public static async Task SaveStorage<T>(this ISessionStorageService sessionStorageService, string key, T value)
        where T : class
    {
        var json = JsonSerializer.Serialize(value);
        await sessionStorageService.SetItemAsStringAsync(key, json);
    }

    public static async Task<T?> GetStorage<T>(this ISessionStorageService sessionStorageService, string key)
        where T : class
    {
        var json = await sessionStorageService.GetItemAsStringAsync(key);
        if (json == null)
            return null;

        return JsonSerializer.Deserialize<T>(json);
    }
}
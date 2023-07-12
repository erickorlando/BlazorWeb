using AdminBaker.Shared.Request;
using AdminBaker.Shared.Response;
using System.Net.Http.Json;
using System.Security;

namespace AdminBaker.Client.Proxy.Services;

public class ProxyUser : IProxyUser
{
    private readonly HttpClient _httpClient;

    public ProxyUser(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<LoginDtoResponse> Login(LoginDtoRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Users/Login", request);
        var loginResponse = await response.Content.ReadFromJsonAsync<LoginDtoResponse>();

        if (loginResponse!.Success)
            return loginResponse;

        throw new SecurityException(loginResponse.ErrorMessage);
    }

    public async Task Register(RegistrarUsuarioDto request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Users/Register", request);
        if (response.IsSuccessStatusCode)
        {
            var resultado = await response.Content.ReadFromJsonAsync<BaseResponse>();
            if (resultado!.Success == false)
                throw new InvalidOperationException(resultado.ErrorMessage);
        }
        else
        {
            throw new InvalidOperationException(response.ReasonPhrase);
        }
    }

    public async Task SendTokenToResetPassword(GenerateTokenToResetDtoRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Users/SendTokenToResetPassword", request);
        if (!response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadFromJsonAsync<BaseResponse>();
            throw new InvalidOperationException(jsonResponse!.ErrorMessage);
        }
    }

    public async Task ResetPassword(ResetPasswordDtoRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Users/ResetPassword", request);
        if (!response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadFromJsonAsync<BaseResponse>();
            throw new InvalidOperationException(jsonResponse!.ErrorMessage);
        }
    }

    public async Task ChangePassword(ChangePasswordDtoRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Users/ChangePassword", request);
        if (!response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadFromJsonAsync<BaseResponse>();
            throw new InvalidOperationException(jsonResponse!.ErrorMessage);
        }
    }

    public async Task UpdateProfile(UpdateProfileDtoRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Users/UpdateProfile", request);
        if (!response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadFromJsonAsync<BaseResponse>();
            throw new InvalidOperationException(jsonResponse!.ErrorMessage);
        }
    }

    public async Task<ClienteDto> GetProfile()
    {
        var response = await _httpClient.GetFromJsonAsync<BaseResponseGeneric<ClienteDto>>("api/Users/GetProfile");
        if (response!.Success)
            return response.Data!;

        throw new InvalidOperationException(response.ErrorMessage);
    }
}
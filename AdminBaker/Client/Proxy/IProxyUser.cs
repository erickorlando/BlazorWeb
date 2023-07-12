using AdminBaker.Shared.Request;
using AdminBaker.Shared.Response;

namespace AdminBaker.Client.Proxy;

public interface IProxyUser
{
    Task<LoginDtoResponse> Login(LoginDtoRequest request);
    Task Register(RegistrarUsuarioDto request);
    Task SendTokenToResetPassword(GenerateTokenToResetDtoRequest request);
    Task ResetPassword(ResetPasswordDtoRequest request);
    Task ChangePassword(ChangePasswordDtoRequest request);
    Task UpdateProfile(UpdateProfileDtoRequest request);
    Task<ClienteDto> GetProfile();
}
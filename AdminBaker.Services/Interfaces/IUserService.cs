using AdminBaker.Shared.Request;
using AdminBaker.Shared.Response;

namespace AdminBaker.Services.Interfaces;

public interface IUserService
{
    Task<LoginDtoResponse> LoginAsync(LoginDtoRequest request);

    Task<BaseResponse> RegisterAsync(RegistrarUsuarioDto request);
    Task<BaseResponse> SendTokenToResetPasswordAsync(GenerateTokenToResetDtoRequest request);
    Task<BaseResponse> ResetPasswordAsync(ResetPasswordDtoRequest request);
    Task<BaseResponse> ChangePasswordAsync(ChangePasswordDtoRequest request);
    Task<BaseResponse> UpdateProfileAsync(UpdateProfileDtoRequest request);
    Task<BaseResponseGeneric<ClienteDto>> GetProfileAsync(string email);
}
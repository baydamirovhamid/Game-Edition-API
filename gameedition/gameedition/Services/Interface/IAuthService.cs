using game.edition.api.DTO.HelperModels.Jwt;
using game.edition.api.DTO.RequestModels.Auth;
using game.edition.api.DTO.ResponseModels.Main;

namespace game.edition.api.Services.Interface
{
    public interface IAuthService
    {
        Task<ResponseSimple> RegisterUserAsync(ResponseSimple response, RegisterDto model);
        Task<ResponseObject<JwtResponse>> LoginAsync(ResponseObject<JwtResponse> response, LoginDto model);
        Task<ResponseSimple> ForgotPasswordAsync(ResponseSimple response, string email);
        Task<ResponseSimple> ResetPasswordAsync(ResponseSimple response, ResetPasswordDto model);
        Task<ResponseSimple> ChangePasswordAsync(ResponseSimple response, ChangePasswordDto model);      
    }
}

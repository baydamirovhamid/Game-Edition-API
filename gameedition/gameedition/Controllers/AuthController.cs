using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using game.edition.api.DTO.HelperModels;
using game.edition.api.DTO.HelperModels.Const;
using game.edition.api.DTO.HelperModels.Jwt;
using game.edition.api.DTO.RequestModels.Auth;
using game.edition.api.DTO.ResponseModels.Main;
using game.edition.api.Services.Interface;
using System.Diagnostics;

namespace game.edition.api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IValidationCommon _validation;
        private readonly ILoggerManager _logger;

        public AuthController(
            IAuthService authService,
            IValidationCommon validation,
            ILoggerManager logger)
        {
            _authService = authService;
            _validation = validation;
            _logger = logger;
        }

        [HttpPost]
        [Route("register-user")]
        public async Task<IActionResult> RegisterUserAsync(RegisterDto model)
        {
            ResponseSimple response = new ResponseSimple();
            response.Status = new StatusModel();
            response.TraceID = Activity.Current?.Id ?? HttpContext?.TraceIdentifier;
            try
            {
                response = await _authService.RegisterUserAsync(response, model);
                if (response.Status.ErrorCode != 0)
                {
                    return StatusCode(_validation.CheckErrorCode(response.Status.ErrorCode), response);
                }
                else
                {
                    return Ok(response);
                }
            }
            catch (Exception e)
            {
                _logger.LogError("TraceId: " + response.TraceID + $", {nameof(RegisterUserAsync)}: " + $"{e}");
                response.Status.ErrorCode = ErrorCodes.SYSTEM;
                response.Status.Message = "Sistemdə xəta baş verdi.";
                return StatusCode(StatusCodeModel.INTERNEL_SERVER, response);
            }
        }


        [HttpPost]
        [Route("login-user")]
        public async Task<IActionResult> LoginUserAsync(LoginDto model)
        {
            ResponseObject<JwtResponse> response = new ResponseObject<JwtResponse>();
            response.Status = new StatusModel();
            response.TraceID = Activity.Current?.Id ?? HttpContext?.TraceIdentifier;
            try
            {
                response = await _authService.LoginAsync(response, model);
                if (response.Status.ErrorCode != 0)
                {
                    return StatusCode(_validation.CheckErrorCode(response.Status.ErrorCode), response);
                }
                else
                {
                    return Ok(response);
                }
            }
            catch (Exception e)
            {
                _logger.LogError("TraceId: " + response.TraceID + $", {nameof(LoginUserAsync)}: " + $"{e}");
                response.Status.ErrorCode = ErrorCodes.SYSTEM;
                response.Status.Message = "Sistemdə xəta baş verdi.";
                return StatusCode(StatusCodeModel.INTERNEL_SERVER, response);
            }
        }

        [HttpPost]
        [Route("forgot-password")]
        public async Task<IActionResult> ForgotPasswordAsync(string email)
        {
            ResponseSimple response = new ResponseSimple();
            response.Status = new StatusModel();
            response.TraceID = Activity.Current?.Id ?? HttpContext?.TraceIdentifier;
            try
            {

                response = await _authService.ForgotPasswordAsync(response, email);
                if (response.Status.ErrorCode != 0)
                {
                    return StatusCode(_validation.CheckErrorCode(response.Status.ErrorCode), response);
                }
                else
                {
                    return Ok(response);
                }
            }
            catch (Exception e)
            {
                _logger.LogError("TraceId: " + response.TraceID + $", {nameof(ForgotPasswordAsync)}: " + $"{e}");
                response.Status.ErrorCode = ErrorCodes.SYSTEM;
                response.Status.Message = "Sistemdə xəta baş verdi.";
                return StatusCode(StatusCodeModel.INTERNEL_SERVER, response);
            }
        }


        [HttpPost]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPasswordAsync(ResetPasswordDto model)
        {
            ResponseSimple response = new ResponseSimple();
            response.Status = new StatusModel();
            response.TraceID = Activity.Current?.Id ?? HttpContext?.TraceIdentifier;
            try
            {

                response = await _authService.ResetPasswordAsync(response, model);
                if (response.Status.ErrorCode != 0)
                {
                    return StatusCode(_validation.CheckErrorCode(response.Status.ErrorCode), response);
                }
                else
                {
                    return Ok(response);
                }
            }
            catch (Exception e)
            {
                _logger.LogError("TraceId: " + response.TraceID + $", {nameof(ResetPasswordAsync)}: " + $"{e}");
                response.Status.ErrorCode = ErrorCodes.SYSTEM;
                response.Status.Message = "Sistemdə xəta baş verdi.";
                return StatusCode(StatusCodeModel.INTERNEL_SERVER, response);
            }
        }

        [HttpPost]
        [Route("change-password")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ChangePasswordAsync(ChangePasswordDto model)
        {
            ResponseSimple response = new ResponseSimple();
            response.Status = new StatusModel();
            response.TraceID = Activity.Current?.Id ?? HttpContext?.TraceIdentifier;
            try
            {
                var userId = Convert.ToInt32(User.Claims.First(x => x.Type == "UserId").Value);
                model.UserId = userId;
                response = await _authService.ChangePasswordAsync(response, model);
                if (response.Status.ErrorCode != 0)
                {
                    return StatusCode(_validation.CheckErrorCode(response.Status.ErrorCode), response);
                }
                else
                {
                    return Ok(response);
                }
            }
            catch (Exception e)
            {
                _logger.LogError("TraceId: " + response.TraceID + $", {nameof(ChangePasswordAsync)}: " + $"{e}");
                response.Status.ErrorCode = ErrorCodes.SYSTEM;
                response.Status.Message = "Sistemdə xəta baş verdi.";
                return StatusCode(StatusCodeModel.INTERNEL_SERVER, response);
            }
        }
    }
}

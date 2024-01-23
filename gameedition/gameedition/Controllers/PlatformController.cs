using Microsoft.AspNetCore.Mvc;
using game.edition.api.DTO.HelperModels.Const;
using game.edition.api.DTO.HelperModels;
using game.edition.api.DTO.ResponseModels.Main;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using System.Diagnostics;
using game.edition.api.Services.Interface;
using game.edition.api.DTO.ResponseModels.Inner;
using game.edition.api.Services.Implementation;
using game.edition.api.DTO.RequestModels;
using game.edition.api.Services.Implementation;
using Microsoft.AspNetCore.JsonPatch;

namespace game.edition.api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private readonly IPlatformService _platformService;
        private readonly IValidationCommon _validation;
        private readonly ILoggerManager _logger;
        public PlatformController(
            IPlatformService platformService,
            IValidationCommon validation,
            ILoggerManager logger
            ) {
            _platformService = platformService;
            _validation = validation;
            _logger = logger;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreatePlatformAsync(PlatformDto model)
        {
            ResponseSimple response = new ResponseSimple();
            response.Status = new StatusModel();
            response.TraceID = Activity.Current?.Id ?? HttpContext?.TraceIdentifier;
            try
            {

                response = await _platformService.CreateAsync(response, model);
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
                _logger.LogError("TraceId: " + response.TraceID + $", {nameof(CreatePlatformAsync)}: " + $"{e}");
                response.Status.ErrorCode = ErrorCodes.SYSTEM;
                response.Status.Message = "Sistemdə xəta baş verdi.";
                return StatusCode(StatusCodeModel.INTERNEL_SERVER, response);
            }
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdatePlatformAsync(PlatformDto model, int id)
        {
            ResponseSimple response = new ResponseSimple();
            response.Status = new StatusModel();
            response.TraceID = Activity.Current?.Id ?? HttpContext?.TraceIdentifier;
            try
            {

                response = await _platformService.UpdateAsync(response, model, id);
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
                _logger.LogError("TraceId: " + response.TraceID + $", {nameof(UpdatePlatformAsync)}: " + $"{e}");
                response.Status.ErrorCode = ErrorCodes.SYSTEM;
                response.Status.Message = "Sistemdə xəta baş verdi.";
                return StatusCode(StatusCodeModel.INTERNEL_SERVER, response);
            }
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeletePlatformAsync(int id)
        {
            ResponseSimple response = new ResponseSimple();
            response.Status = new StatusModel();
            response.TraceID = Activity.Current?.Id ?? HttpContext?.TraceIdentifier;
            try
            {

                response = await _platformService.DeleteAsync(response, id);
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
                _logger.LogError("TraceId: " + response.TraceID + $", {nameof(DeletePlatformAsync)}: " + $"{e}");
                response.Status.ErrorCode = ErrorCodes.SYSTEM;
                response.Status.Message = "Sistemdə xəta baş verdi.";
                return StatusCode(StatusCodeModel.INTERNEL_SERVER, response);
            }
        }
        [HttpGet]
        [Route("get-by-id")]
        public async Task<IActionResult> GetById(int id)
        {
            ResponseObject<PlatformVM> response = new ResponseObject<PlatformVM>();
            response.Status = new StatusModel();
            response.TraceID = Activity.Current?.Id ?? HttpContext?.TraceIdentifier;
            try
            {
                response.Response = await _platformService.GetByIdAsync(id);
                if (response.Response == null)
                {
                    response.Status.Message = "Məlumat tapılmadı!";
                    response.Status.ErrorCode = ErrorCodes.NOT_FOUND;
                    StatusCode(_validation.CheckErrorCode(response.Status.ErrorCode), response);
                }
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError("TraceId: " + response.TraceID + $", {nameof(GetById)}: " + $"{e}");
                response.Status.ErrorCode = ErrorCodes.SYSTEM;
                response.Status.Message = "Sistemdə xəta baş verdi.";
                return StatusCode(StatusCodeModel.INTERNEL_SERVER, response);
            }
        }

        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> GetAll(int page, int pageSize)
        {
            ResponseListTotal<PlatformVM> response = new ResponseListTotal<PlatformVM>();
            response.Response = new ResponseTotal<PlatformVM>();
            response.Status = new StatusModel();
            response.TraceID = Activity.Current?.Id ?? HttpContext?.TraceIdentifier;
            try
            {
                response = await _platformService.GetAll(response, page, pageSize);
                if (response.Response.Data == null)
                {
                    response.Status.Message = "Məlumat tapılmadı!";
                    response.Status.ErrorCode = ErrorCodes.NOT_FOUND;
                    StatusCode(_validation.CheckErrorCode(response.Status.ErrorCode), response);
                }
                return Ok(response);
            }
            catch (Exception e)
            {
                _logger.LogError("TraceId: " + response.TraceID + $", {nameof(GetAll)}: " + $"{e}");
                response.Status.ErrorCode = ErrorCodes.SYSTEM;
                response.Status.Message = "Sistemdə xəta baş verdi.";
                return StatusCode(StatusCodeModel.INTERNEL_SERVER, response);
            }
        }
        [HttpPut]
        [Route("update-platform")]
        public async Task<IActionResult> UpdatePlatformAsync(int id, [FromBody] PlatformDto model)
        {
            ResponseSimple response = new ResponseSimple();
            response.Status = new StatusModel();
            response.TraceID = Activity.Current?.Id ?? HttpContext?.TraceIdentifier;
            try
            {
                response = await _platformService.UpdatePlatformAsync(response, model, id);
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
                _logger.LogError("TraceId: " + response.TraceID + $", {nameof(UpdatePlatformAsync)}: " + $"{e}");
                response.Status.ErrorCode = ErrorCodes.SYSTEM;
                response.Status.Message = "An error occurred in the system.";
                return StatusCode(StatusCodeModel.INTERNEL_SERVER, response);
            }
        }

        [HttpPatch]
        [Route("update-platform-patch")]
        public async Task<IActionResult> PartiallyUpdateProduct(int id, [FromBody] JsonPatchDocument<PlatformDto> model)
        {
            ResponseSimple response = new ResponseSimple();
            response.Status = new StatusModel();
            response.TraceID = Activity.Current?.Id ?? HttpContext?.TraceIdentifier;
            try
            {
                response = await _platformService.PartiallyUpdatePlatformAsync(response, id, model);
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
                _logger.LogError("TraceId: " + response.TraceID + $", {nameof(UpdatePlatformAsync)}: " + $"{e}");
                response.Status.ErrorCode = ErrorCodes.SYSTEM;
                response.Status.Message = "An error occurred in the system.";
                return StatusCode(StatusCodeModel.INTERNEL_SERVER, response);
            }
        }


    }
   
}

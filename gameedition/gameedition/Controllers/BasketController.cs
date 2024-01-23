using Microsoft.AspNetCore.Mvc;
using game.edition.api.DTO.HelperModels.Const;
using game.edition.api.DTO.HelperModels;
using game.edition.api.DTO.RequestModels;
using game.edition.api.DTO.ResponseModels.Main;
using game.edition.api.Services.Interface;
using System.Diagnostics;
using game.edition.api.Services.Implementation;

namespace game.edition.api.Controllers
{
   
        [Route("api/v1/[controller]")]
        [ApiController]
        public class BasketController : ControllerBase
        {
            private readonly IBasketService _basketService;
            private readonly IValidationCommon _validation;
            private readonly ILoggerManager _logger;
            public BasketController(
                IBasketService basketService,
                IValidationCommon validation,
                ILoggerManager logger
                )
            {
                _basketService = basketService;
                _validation = validation;
                _logger = logger;
            }

            [HttpPost]
            [Route("create")]
            public async Task<IActionResult> CreateBasketAsync(BasketDto model)
            {
                ResponseSimple response = new ResponseSimple();
                response.Status = new StatusModel();
                response.TraceID = Activity.Current?.Id ?? HttpContext?.TraceIdentifier;
                try
                {

                    response = await _basketService.CreateAsync(response, model);
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
                    _logger.LogError("TraceId: " + response.TraceID + $", {nameof(CreateBasketAsync)}: " + $"{e}");
                    response.Status.ErrorCode = ErrorCodes.SYSTEM;
                    response.Status.Message = "Sistemdə xəta baş verdi.";
                    return StatusCode(StatusCodeModel.INTERNEL_SERVER, response);
                }
            }

            [HttpPost]
            [Route("update")]
            public async Task<IActionResult> UpdateBasketAsync(BasketDto model, int id)
            {
                ResponseSimple response = new ResponseSimple();
                response.Status = new StatusModel();
                response.TraceID = Activity.Current?.Id ?? HttpContext?.TraceIdentifier;
                try
                {

                    response = await _basketService.UpdateAsync(response, model, id);
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
                    _logger.LogError("TraceId: " + response.TraceID + $", {nameof(UpdateBasketAsync)}: " + $"{e}");
                    response.Status.ErrorCode = ErrorCodes.SYSTEM;
                    response.Status.Message = "Sistemdə xəta baş verdi.";
                    return StatusCode(StatusCodeModel.INTERNEL_SERVER, response);
                }
            }

            [HttpDelete]
            [Route("delete")]
            public async Task<IActionResult> DeleteBasketAsync(int id)
            {
                ResponseSimple response = new ResponseSimple();
                response.Status = new StatusModel();
                response.TraceID = Activity.Current?.Id ?? HttpContext?.TraceIdentifier;
                try
                {

                    response = await _basketService.DeleteAsync(response, id);
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
                    _logger.LogError("TraceId: " + response.TraceID + $", {nameof(DeleteBasketAsync)}: " + $"{e}");
                    response.Status.ErrorCode = ErrorCodes.SYSTEM;
                    response.Status.Message = "Sistemdə xəta baş verdi.";
                    return StatusCode(StatusCodeModel.INTERNEL_SERVER, response);
                }
            }


        [HttpGet]
        [Route("get-by-id")]
        public async Task<IActionResult> GetById(int id)
        {
            ResponseObject<BasketVM> response = new ResponseObject<BasketVM>();
            response.Status = new StatusModel();
            response.TraceID = Activity.Current?.Id ?? HttpContext?.TraceIdentifier;
            try
            {
                response.Response = await _basketService.GetByIdAsync(id);
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
            ResponseListTotal<BasketVM> response = new ResponseListTotal<BasketVM>();
            response.Response = new ResponseTotal<BasketVM>();
            response.Status = new StatusModel();
            response.TraceID = Activity.Current?.Id ?? HttpContext?.TraceIdentifier;
            try
            {
                response = await _basketService.GetAll(response, page, pageSize);
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
    }
 }


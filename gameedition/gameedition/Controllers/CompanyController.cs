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
using Microsoft.AspNetCore.JsonPatch;

namespace game.edition.api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly IValidationCommon _validation;
        private readonly ILoggerManager _logger;
        public CompanyController(
            ICompanyService companyService,
            IValidationCommon validation,
            ILoggerManager logger
            ) {
            _companyService = companyService;
            _validation = validation;
            _logger = logger;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateCompanyAsync(CompanyDto model)
        {
            ResponseSimple response = new ResponseSimple();
            response.Status = new StatusModel();
            response.TraceID = Activity.Current?.Id ?? HttpContext?.TraceIdentifier;
            try
            {

                response = await _companyService.CreateAsync(response, model);
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
                _logger.LogError("TraceId: " + response.TraceID + $", {nameof(CreateCompanyAsync)}: " + $"{e}");
                response.Status.ErrorCode = ErrorCodes.SYSTEM;
                response.Status.Message = "Sistemdə xəta baş verdi.";
                return StatusCode(StatusCodeModel.INTERNEL_SERVER, response);
            }
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateCompanyAsync(CompanyDto model, int id)
        {
            ResponseSimple response = new ResponseSimple();
            response.Status = new StatusModel();
            response.TraceID = Activity.Current?.Id ?? HttpContext?.TraceIdentifier;
            try
            {

                response = await _companyService.UpdateAsync(response, model, id);
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
                _logger.LogError("TraceId: " + response.TraceID + $", {nameof(UpdateCompanyAsync)}: " + $"{e}");
                response.Status.ErrorCode = ErrorCodes.SYSTEM;
                response.Status.Message = "Sistemdə xəta baş verdi.";
                return StatusCode(StatusCodeModel.INTERNEL_SERVER, response);
            }
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteCompanyAsync(int id)
        {
            ResponseSimple response = new ResponseSimple();
            response.Status = new StatusModel();
            response.TraceID = Activity.Current?.Id ?? HttpContext?.TraceIdentifier;
            try
            {

                response = await _companyService.DeleteAsync(response, id);
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
                _logger.LogError("TraceId: " + response.TraceID + $", {nameof(DeleteCompanyAsync)}: " + $"{e}");
                response.Status.ErrorCode = ErrorCodes.SYSTEM;
                response.Status.Message = "Sistemdə xəta baş verdi.";
                return StatusCode(StatusCodeModel.INTERNEL_SERVER, response);
            }
        }

        [HttpGet]
        [Route("get-by-id")]
        public async Task<IActionResult> GetById(int id)
        {
            ResponseObject<CompanyVM> response = new ResponseObject<CompanyVM>();
            response.Status = new StatusModel();
            response.TraceID = Activity.Current?.Id ?? HttpContext?.TraceIdentifier;
            try
            {
                response.Response = await _companyService.GetByIdAsync(id);
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
            ResponseListTotal<CompanyVM> response = new ResponseListTotal<CompanyVM>();
            response.Response = new ResponseTotal<CompanyVM>();
            response.Status = new StatusModel();
            response.TraceID = Activity.Current?.Id ?? HttpContext?.TraceIdentifier;
            try
            {
                response = await _companyService.GetAll(response, page, pageSize);
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
        [Route("update-company")]
        public async Task<IActionResult> UpdateCompaUpdateCompanyAsyncnyAsync(int id, [FromBody] CompanyDto model)
        {
            ResponseSimple response = new ResponseSimple();
            response.Status = new StatusModel();
            response.TraceID = Activity.Current?.Id ?? HttpContext?.TraceIdentifier;
            try
            {
                response = await _companyService.UpdateCompanyAsync(response, model, id);
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
                _logger.LogError("TraceId: " + response.TraceID + $", {nameof(UpdateCompanyAsync)}: " + $"{e}");
                response.Status.ErrorCode = ErrorCodes.SYSTEM;
                response.Status.Message = "An error occurred in the system.";
                return StatusCode(StatusCodeModel.INTERNEL_SERVER, response);
            }
        }

        [HttpPatch]
        [Route("update-company-patch")]
        public async Task<IActionResult> PartiallyUpdateProduct(int id, [FromBody] JsonPatchDocument<CompanyDto> model)
        {
            ResponseSimple response = new ResponseSimple();
            response.Status = new StatusModel();
            response.TraceID = Activity.Current?.Id ?? HttpContext?.TraceIdentifier;
            try
            {
                response = await _companyService.PartiallyUpdateCompanyAsync(response, id, model);
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
                _logger.LogError("TraceId: " + response.TraceID + $", {nameof(UpdateCompanyAsync)}: " + $"{e}");
                response.Status.ErrorCode = ErrorCodes.SYSTEM;
                response.Status.Message = "An error occurred in the system.";
                return StatusCode(StatusCodeModel.INTERNEL_SERVER, response);
            }
        }

        
    }
}

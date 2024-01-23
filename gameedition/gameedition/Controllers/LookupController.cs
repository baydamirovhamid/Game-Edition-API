using Microsoft.AspNetCore.Mvc;
using game.edition.api.DTO.HelperModels.Const;
using game.edition.api.DTO.HelperModels;
using game.edition.api.DTO.ResponseModels.Main;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using System.Diagnostics;
using game.edition.api.Services.Interface;
using game.edition.api.DTO.ResponseModels.Inner;


namespace game.edition.api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LookupController : ControllerBase
    {
        private readonly ILookupService _lookupService;
        public LookupController(ILookupService lookupService) {
            _lookupService = lookupService;
        }

        [HttpGet]
        [Route("get-static-data")]
        public async Task<IActionResult> GetStaticData(string key)
        {
            ResponseObject<StaticVM> response = new ResponseObject<StaticVM>();
            response.Status = new StatusModel();
            response.TraceID = Activity.Current?.Id ?? HttpContext?.TraceIdentifier;
            try
            {
                response = await _lookupService.GetStaticDataAsync(response, key);
                return Ok(response);
            }
            catch (Exception e)
            {

                response.Status.ErrorCode = ErrorCodes.SYSTEM;
                response.Status.Message = "Sistemdə xəta baş verdi.";
                return StatusCode(StatusCodeModel.INTERNEL_SERVER, response);
            }
        }

        [HttpGet]
        [Route("get-game")]
        public async Task<IActionResult> GetGameData()
        {
            ResponseList<GameVM> response = new ResponseList<GameVM>();
            response.Status = new StatusModel();
            response.TraceID = Activity.Current?.Id ?? HttpContext?.TraceIdentifier;
            try
            {
                response = await _lookupService.GetGameAsync(response);
                return Ok(response);
            }
            catch (Exception e)
            {

                response.Status.ErrorCode = ErrorCodes.SYSTEM;
                response.Status.Message = "Sistemdə xəta baş verdi.";
                return StatusCode(StatusCodeModel.INTERNEL_SERVER, response);
            }
        }
        [HttpGet]
        [Route("get-company")]
        public async Task<IActionResult> GetCompanyData()
        {
            ResponseList<CompanyVM> response = new ResponseList<CompanyVM>();
            response.Status = new StatusModel();
            response.TraceID = Activity.Current?.Id ?? HttpContext?.TraceIdentifier;
            try
            {
                response = await _lookupService.GetCompanyAsync(response);
                return Ok(response);
            }
            catch (Exception e)
            {

                response.Status.ErrorCode = ErrorCodes.SYSTEM;
                response.Status.Message = "Sistemdə xəta baş verdi.";
                return StatusCode(StatusCodeModel.INTERNEL_SERVER, response);
            }
        }

        [HttpGet]
        [Route("get-platform")]
        public async Task<IActionResult> GetPlatform()
        {
            ResponseList<PlatformVM> response = new ResponseList<PlatformVM>();
            response.Status = new StatusModel();
            response.TraceID = Activity.Current?.Id ?? HttpContext?.TraceIdentifier;
            try
            {
                response = await _lookupService.GetPlatformAsync(response);
                return Ok(response);
            }
            catch (Exception e)
            {

                response.Status.ErrorCode = ErrorCodes.SYSTEM;
                response.Status.Message = "Sistemdə xəta baş verdi.";
                return StatusCode(StatusCodeModel.INTERNEL_SERVER, response);
            }
        }
    }
}

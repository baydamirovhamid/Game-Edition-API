using game.edition.api.DTO.ResponseModels.Inner;
using game.edition.api.DTO.ResponseModels.Main;
using game.edition.api.Services.Implementation;

namespace game.edition.api.Services.Interface
{
    public interface ILookupService
    {
        Task<ResponseObject<StaticVM>> GetStaticDataAsync(ResponseObject<StaticVM> response, string key);
        Task<ResponseList<GameVM>> GetGameAsync(ResponseList<GameVM> response);
        Task<ResponseList<PlatformVM>> GetPlatformAsync(ResponseList<PlatformVM> response);
        Task<ResponseList<CompanyVM>> GetCompanyAsync(ResponseList<CompanyVM> response);
        Task<ResponseList<GameCompanyVM>> GetGameCompany(ResponseList<GameCompanyVM> response);
        Task<ResponseList<GamePlatformVM>> GetGamePlatform(ResponseList<GamePlatformVM> response);
    }
}

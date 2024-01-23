using Microsoft.AspNetCore.JsonPatch;
using game.edition.api.DTO.RequestModels;
using game.edition.api.DTO.ResponseModels.Inner;
using game.edition.api.DTO.ResponseModels.Main;
using game.edition.api.Services.Implementation;

namespace game.edition.api.Services.Interface
{
    public interface ICompanyService
    {
        Task<ResponseSimple> CreateAsync(ResponseSimple response, CompanyDto model);
        Task<ResponseSimple> UpdateAsync(ResponseSimple response, CompanyDto model, int id);
        Task<ResponseSimple> DeleteAsync(ResponseSimple response, int id);
        Task<CompanyVM> GetByIdAsync(int id);
        Task<ResponseListTotal<CompanyVM>> GetAll(ResponseListTotal<CompanyVM> response, int page, int pageSize);
        Task<ResponseSimple> UpdateCompanyAsync(ResponseSimple response, CompanyDto model, int id);
        Task<ResponseSimple> PartiallyUpdateCompanyAsync(ResponseSimple response, int id, JsonPatchDocument<CompanyDto> model);
    }
}

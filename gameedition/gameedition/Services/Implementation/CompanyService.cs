using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using game.edition.api.DTO.HelperModels.Const;
using game.edition.api.DTO.RequestModels;
using game.edition.api.DTO.ResponseModels.Inner;
using game.edition.api.DTO.ResponseModels.Main;
using game.edition.api.Infrastructure.Repository;
using game.edition.api.Models;
using game.edition.api.Services.Interface;

namespace game.edition.api.Services.Implementation
{
    public class CompanyService : ICompanyService
    {
        private readonly IRepository<COMPANY> _companies;
        private readonly ILoggerManager _logger;
        private readonly IConfiguration _configuration;
        private readonly ISqlService _sqlService;
        private readonly IMapper _mapper;

        public CompanyService(
            IRepository<COMPANY> companies,
            ILoggerManager logger,
            IConfiguration configuration,
            ISqlService sqlService,
            IMapper mapper)
        {
            _companies = companies;
            _logger = logger;
            _configuration = configuration;
            _sqlService = sqlService;
            _mapper = mapper;
        }

        public async Task<ResponseSimple> CreateAsync(ResponseSimple response, CompanyDto model)
        {
            try
            {
                var company = _mapper.Map<COMPANY>(model);
                company.CreatedAt = DateTime.Now;
                _companies.Insert(company);
                await _companies.SaveAsync();
                response.Status.Message = "Uğurla əlavə olundu!";
            }
            catch (Exception e)
            {
                _logger.LogError("TraceId: " + response.TraceID + $", {nameof(CreateAsync)}: " + $"{e}");
                response.Status.ErrorCode = ErrorCodes.DB;
                response.Status.Message = "Problem baş verdi!";
            }
            return response;
        }

        public async Task<ResponseSimple> UpdateAsync(ResponseSimple response, CompanyDto model, int id)
        {
            try
            {
                var company = _mapper.Map<COMPANY>(model);

                var companyDb = await _companies.AllQuery
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);

                company.Id = id;
                company.UpdatedAt = DateTime.Now;
                company.CreatedAt = companyDb.CreatedAt;

                _companies.Update(company);
                await _companies.SaveAsync();
                response.Status.Message = "Uğurla yeniləndi!";
            }
            catch (Exception e)
            {
                _logger.LogError("TraceId: " + response.TraceID + $", {nameof(UpdateAsync)}: " + $"{e}");
                response.Status.ErrorCode = ErrorCodes.DB;
                response.Status.Message = "Problem baş verdi!";
            }
            return response;
        }

        public async Task<ResponseSimple> DeleteAsync(ResponseSimple response, int id)
        {
            try
            {
                var company = await _companies.AllQuery.FirstOrDefaultAsync(x => x.Id == id);
                _companies.Remove(company);
                await _companies.SaveAsync();
                response.Status.Message = "Uğurla silindi!";
            }
            catch (Exception e)
            {
                _logger.LogError("TraceId: " + response.TraceID + $", {nameof(DeleteAsync)}: " + $"{e}");
                response.Status.ErrorCode = ErrorCodes.DB;
                response.Status.Message = "Problem baş verdi!";
            }
            return response;
        }


        public async Task<CompanyVM> GetByIdAsync(int id)
        {
            var db_model = await _companies.AllQuery.FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<CompanyVM>(db_model);
        }

        public async Task<ResponseListTotal<CompanyVM>> GetAll(ResponseListTotal<CompanyVM> response, int page, int pageSize)
        {
            
            var db_data = await _companies.AllQuery.OrderByDescending(x=>x.CreatedAt).ToListAsync();
            response.Response.Total = db_data.Count;
            db_data = db_data.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            response.Response.Data = _mapper.Map<List<CompanyVM>>(db_data);
            return response;
        }


        public async Task<ResponseSimple> UpdateCompanyAsync(ResponseSimple response, CompanyDto model, int id)
        {
            try
            {
                var company = _mapper.Map<COMPANY>(model);
                var existingCompany = await _companies.AllQuery
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
                company.Id = id;
                _companies.Update(company);
                await _companies.SaveAsync();
                response.Status.ErrorCode = 0;
                response.Status.Message = "Uğurla yeniləndi!";
            }

            catch (Exception e)
            {
                _logger.LogError("TraceId: " + response.TraceID + $", {nameof(UpdateCompanyAsync)}: " + $"{e}");
                response.Status.ErrorCode = ErrorCodes.DB;
                response.Status.Message = "Problem baş verdi!";
            }

            return response;
        }


        public async Task<ResponseSimple> PartiallyUpdateCompanyAsync(ResponseSimple response, int id, JsonPatchDocument<CompanyDto> model)
        {
            try
            {
                var existingCompany = await _companies.AllQuery
                .FirstOrDefaultAsync(x => x.Id == id);

                if (existingCompany == null)
                {
                    response.Status.ErrorCode = ErrorCodes.NOT_FOUND;
                    response.Status.Message = "Company not found.";
                    return response;
                }

                var companyDto = _mapper.Map<CompanyDto>(existingCompany);

                model.ApplyTo(companyDto);

                _mapper.Map(companyDto, existingCompany);
                _companies.Update(existingCompany);
                await _companies.SaveAsync();
                response.Status.ErrorCode = 0;
                response.Status.Message = "Uğurla yeniləndi!";
            }

            catch (Exception e)
            {
                _logger.LogError("TraceId: " + response.TraceID + $", {nameof(UpdateCompanyAsync)}: " + $"{e}");
                response.Status.ErrorCode = ErrorCodes.DB;
                response.Status.Message = "Problem baş verdi!";
            }
            return response;
        }
    }
}

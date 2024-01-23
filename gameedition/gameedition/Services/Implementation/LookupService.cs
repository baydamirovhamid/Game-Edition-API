using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using game.edition.api.DTO.HelperModels.Const;
using game.edition.api.DTO.ResponseModels.Inner;
using game.edition.api.DTO.ResponseModels.Main;
using game.edition.api.Infrastructure.Repository;
using game.edition.api.Models;
using game.edition.api.Services.Interface;

namespace game.edition.api.Services.Implementation
{
    public class LookupService : ILookupService
    {
        private readonly IRepository<STATIC_DATA> _staticDataRepository;
        private readonly IRepository<GAME> _gameRepository;
        private readonly IRepository<PLATFORM> _platformRepository;
        private readonly IRepository<COMPANY> _companyRepository; 
        private readonly IRepository<GAME_COMPANY> _gameCompanyRepository;
        private readonly IRepository<GAME_PLATFORM> _gamePlatformRepository;

        private readonly IMapper _mapper;
        public LookupService(IRepository<STATIC_DATA> staticDataRepository, IRepository<GAME> gameRepository, IRepository<PLATFORM> platformRepository,  IRepository<COMPANY> companyRepository, IRepository<GAME_COMPANY> gameCompanyRepository, IRepository<GAME_PLATFORM> gamePlatformRepository, IMapper mapper)
        {
            _staticDataRepository = staticDataRepository;
            _gameRepository = gameRepository;
            _platformRepository = platformRepository;
            _companyRepository = companyRepository;
            _gameCompanyRepository = gameCompanyRepository;
            _gamePlatformRepository = gamePlatformRepository;
            _mapper = mapper;
        }

        
        public async Task<ResponseObject<StaticVM>> GetStaticDataAsync(ResponseObject<StaticVM> response, string key)
        {
            try
            {
                var result = await _staticDataRepository.AllQuery.FirstOrDefaultAsync(x => x.Key == key);
                if (result == null)
                {
                    response.Status.ErrorCode = ErrorCodes.NOT_FOUND;
                    response.Status.Message = "Tapılmadı!";
                }
                else
                {
                    response.Response = _mapper.Map<StaticVM>(result);
                }
            }
            catch (Exception ex)
            {
                response.Status.ErrorCode = ErrorCodes.DB;
                response.Status.Message = "Problem baş verdi!";
            }
            return response;
        }
    
       public async Task<ResponseList<GameVM>> GetGameAsync(ResponseList<GameVM> response)
    {
        try
        {
            var result = await _gameRepository.AllQuery.ToListAsync();
          
            response.Data = _mapper.Map<List<GameVM>>(result);
            
        }
        catch (Exception ex)
        {
            response.Status.ErrorCode = ErrorCodes.DB;
            response.Status.Message = "Problem baş verdi!";
        }
        return response;
    }

        public async Task<ResponseList<PlatformVM>> GetPlatformAsync(ResponseList<PlatformVM> response)
        {
            try
            {
                var result = await _platformRepository.AllQuery.ToListAsync();
                response.Data = _mapper.Map<List<PlatformVM>>(result);
                 
            }
            catch (Exception ex)
            {
                response.Status.ErrorCode = ErrorCodes.DB;
                response.Status.Message = "Problem baş verdi!";
            }
            return response;
        }
        public async Task<ResponseList<CompanyVM>> GetCompanyAsync(ResponseList<CompanyVM> response)
        {
            try
            {
                var result = await _companyRepository.AllQuery.ToListAsync();

                response.Data = _mapper.Map<List<CompanyVM>>(result);

            }
            catch (Exception ex)
            {
                response.Status.ErrorCode = ErrorCodes.DB;
                response.Status.Message = "Problem baş verdi!";
            }
            return response;
        }


        public async Task<ResponseList<GameCompanyVM>> GetGameCompany(ResponseList<GameCompanyVM> response)
        {
            try
            {
                var result = await _gameCompanyRepository.AllQuery.ToListAsync();
                response.Data = _mapper.Map<List<GameCompanyVM>>(result);
              
            }
            catch (Exception ex)
            {
                response.Status.ErrorCode = ErrorCodes.DB;
                response.Status.Message = "Problem baş verdi!";
            }
            return response;
        }
        public async Task<ResponseList<GamePlatformVM>> GetGamePlatform(ResponseList<GamePlatformVM> response)
        {
            try
            {
                var result = await _gamePlatformRepository.AllQuery.ToListAsync();
                response.Data = _mapper.Map<List<GamePlatformVM>>(result);

            }
            catch (Exception ex)
            {
                response.Status.ErrorCode = ErrorCodes.DB;
                response.Status.Message = "Problem baş verdi!";
            }
            return response;
        }
    }
}


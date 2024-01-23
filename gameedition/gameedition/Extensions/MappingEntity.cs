using AutoMapper;
using game.edition.api.DTO.HelperModels.Jwt;
using game.edition.api.DTO.RequestModels;
using game.edition.api.DTO.RequestModels.Auth;
using game.edition.api.DTO.ResponseModels.Inner;
using game.edition.api.Models;

namespace game.edition.api.Extensions
{
    public class MappingEntity: Profile
    {
        public MappingEntity()
        {
            CreateMap<STATIC_DATA, StaticVM>().ReverseMap();
            CreateMap<PLATFORM, PlatformVM>().ReverseMap();
            CreateMap<GAME, GameVM>().ReverseMap();
            CreateMap<COMPANY, CompanyVM>().ReverseMap();
            CreateMap<BASKET, BasketVM>().ReverseMap();
            CreateMap<GAME_PLATFORM, GamePlatformVM>().ReverseMap();
            CreateMap<GAME_COMPANY, GameCompanyVM>().ReverseMap();
           

            CreateMap<BASKET, BasketDto>().ReverseMap();
            CreateMap<COMPANY, CompanyDto>().ReverseMap();
            CreateMap<GAME, GameDto>().ReverseMap();
            CreateMap<PLATFORM, PlatformDto>().ReverseMap();
           
            CreateMap<USER, RegisterDto>().ReverseMap();
            CreateMap<USER, JwtCustomClaims>()
                .ForMember(dest => dest.UserId, opts => opts.MapFrom(src => src.Id))
                .ReverseMap();

        }
    }
}

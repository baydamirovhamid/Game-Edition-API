using game.edition.api.DTO.HelperModels;
using game.edition.api.DTO.HelperModels.Jwt;
using game.edition.api.Models;

namespace game.edition.api.Services.Interface
{
    public interface IJwtHandler
    {
        JwtResponse CreateToken(JwtCustomClaims claims);
    }
}

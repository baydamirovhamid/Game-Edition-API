namespace game.edition.api.DTO.HelperModels.Jwt
{
    public class JwtResponse
    {
        public string Token { get; set; }
        public long ExpiresAt { get; set; }
    }
}

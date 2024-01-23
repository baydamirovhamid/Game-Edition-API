namespace game.edition.api.DTO.ResponseModels.Inner
{
    public class PlatformVM
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? ReleaseDate { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}

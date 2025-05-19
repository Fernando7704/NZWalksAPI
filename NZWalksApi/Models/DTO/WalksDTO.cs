namespace NZWalksApi.Models.DTO
{
    public class WalksDTO
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string descripcion { get; set; }
        public double lengthInKm { get; set; }
        public string walkImageUrl { get; set; }

        //public Guid difficultyId { get; set; }
        //public Guid regionId { get; set; }
        public DifficultyDTO difficulty { get; set; }
        public RegionDTO region { get; set; }
    }
}

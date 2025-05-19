namespace NZWalksApi.Models.Domain
{
    public class Walk
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string descripcion { get; set; }
        public double lengthInKm { get; set; }
        public string walkImageUrl { get; set; }

        public Guid difficultyId { get; set; }
        public Guid regionId { get; set; }


        //Navigation propietaries

        public Difficulty difficulty { get; set; }
        public Region region { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace NZWalksApi.Models.DTO
{
    public class addRequestWalkDTO
    {
        [Required]
        [MaxLength(100,ErrorMessage ="")]
        public string name { get; set; }
        [Required]
        [MaxLength(1000)]
        public string descripcion { get; set; }
        [Required]
        [Range(0,50)]
        public double lengthInKm { get; set; }
        public string walkImageUrl { get; set; }
        [Required]
        public Guid difficultyId { get; set; }
        [Required]
        public Guid regionId { get; set; }
    }
}

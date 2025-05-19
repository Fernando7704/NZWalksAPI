using System.ComponentModel.DataAnnotations;

namespace NZWalksApi.Models.DTO
{
    public class UpdateWalkRequestDTO
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Maximo 100 caracteres")]
        public string name { get; set; }
        [Required]
        [MaxLength(1000,ErrorMessage ="Maximo 1000 caracteres")]
        public string descripcion { get; set; }
        [Required]
        [Range(0, 50)]
        public double lengthInKm { get; set; }
        public string walkImageUrl { get; set; }
        [Required]
        public Guid difficultyId { get; set; }
        [Required]
        public Guid regionId { get; set; }
    }
}

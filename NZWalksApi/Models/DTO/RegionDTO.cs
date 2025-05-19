using System.ComponentModel.DataAnnotations;

namespace NZWalksApi.Models.DTO
{
    public class RegionDTO
    {
        public Guid Id { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "El número de caracteres debe ser 3 caracteres, los caracteres ingresados son menores.")]
        [MaxLength(3, ErrorMessage = "El código debe de ser 3 caracteres, los caracteres ingresados son mayores.")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "El nombre debe de ser maximo 100 caracteres")]
        public string Name { get; set; }
        public string? RegionImagenUrl { get; set; }
    }
}

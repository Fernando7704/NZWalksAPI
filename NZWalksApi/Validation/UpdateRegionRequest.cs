using FluentValidation;

namespace NZWalksApi.Validation
{
    public class UpdateRegionRequest : AbstractValidator<Models.DTO.RegionDTO>
    {
        public UpdateRegionRequest()
        {
            RuleFor(x => x.Code)
                .NotEmpty()
                .WithMessage("No poner valores vacios!!!");
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name cannot be empty");
            RuleFor(x => x.RegionImagenUrl)
                .NotEmpty()
                .WithMessage("Region Image URL cannot be empty");
        }
    }
    
}

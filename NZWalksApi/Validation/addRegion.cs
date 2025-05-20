using FluentValidation;

namespace NZWalksApi.Validation
{
    public class addRegion : AbstractValidator<Models.DTO.addRegionRequestDto>
    {
        public addRegion() 
        {
            RuleFor(x => x.Code)
                .NotEmpty()
                .WithMessage("Code is required jejej")
                .MinimumLength(3)
                .WithMessage("Code must be at least 3 characters long")
                .MaximumLength(3)
                .WithMessage("Code must be at most 3 characters long");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .MinimumLength(3)
                .WithMessage("Name must be at least 3 characters long")
                .MaximumLength(100)
                .WithMessage("Name must be at most 100 characters long");

            RuleFor(x => x.RegionImagenUrl)
                .NotEmpty()
                .WithMessage("Region Image URL is required");

        }  
    }
}

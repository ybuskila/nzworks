using FluentValidation;

namespace nzworks.api.Validators
{
    public class UpdateWalkRequestValidator : AbstractValidator<Models.DTO.UpdateWalkRequest>
    {
        public UpdateWalkRequestValidator()
        {
            RuleFor(x => x.Length).GreaterThan(0);
            RuleFor(x => x.FullName).NotEmpty();
        }
    }
}

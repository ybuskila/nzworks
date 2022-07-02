using FluentValidation;

namespace nzworks.api.Validators
{
    public class AddWalkRequestValidator  : AbstractValidator<Models.DTO.AddWalkRequest>
    {
        public AddWalkRequestValidator()
        {
            RuleFor(x => x.Length).GreaterThan(0);
            RuleFor(x => x.FullName).NotEmpty();
        }
    }
}

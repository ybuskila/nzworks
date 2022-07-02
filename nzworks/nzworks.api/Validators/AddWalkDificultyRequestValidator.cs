using FluentValidation;

namespace nzworks.api.Validators
{
    public class AddWalkDificultyRequestValidator : AbstractValidator<Models.DTO.AddWalkDificultyRequest>
    {
        public AddWalkDificultyRequestValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
        }
    }
}

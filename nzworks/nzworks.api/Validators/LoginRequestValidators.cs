using FluentValidation;

namespace nzworks.api.Validators
{
    public class LoginRequestValidators : AbstractValidator<Models.DTO.LoginRequest>
    {
        public LoginRequestValidators()
        {
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}

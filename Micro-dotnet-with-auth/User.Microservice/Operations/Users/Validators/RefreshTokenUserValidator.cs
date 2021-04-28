using FluentValidation;
using User.Microservice.Operations.User.ViewModels;

namespace User.Microservice.Operations.User.Validator{
    public class RefreshTokenUserValidator : AbstractValidator<RefreshTokenUserViewModel> {
        public RefreshTokenUserValidator() {

            RuleFor(user => user.RefreshToken)
                    .NotEmpty()
                    .NotNull();
            
        }


    }
}

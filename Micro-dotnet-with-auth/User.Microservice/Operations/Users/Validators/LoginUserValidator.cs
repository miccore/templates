using FluentValidation;
using User.Microservice.Operations.User.ViewModels;

namespace User.Microservice.Operations.User.Validator{
    public class LoginUserValidator : AbstractValidator<LoginUserViewModel> {
        public LoginUserValidator() {

            
            RuleFor(user => user.Phone)
                    .NotEmpty()
                    .NotNull();
            
            RuleFor(user => user.Password)
                    .NotEmpty()
                    .NotNull();
            
        }


    }
}

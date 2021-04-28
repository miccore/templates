using FluentValidation;
using User.Microservice.Operations.User.ViewModels;

namespace User.Microservice.Operations.User.Validator{
    public class UserPasswordValidator : AbstractValidator<UserPasswordViewModel> {
        public UserPasswordValidator() {

            
            RuleFor(User => User.odlpassword)
                    .NotEmpty()
                    .NotNull();
            
            RuleFor(User => User.newpassword)
                    .NotEmpty()
                    .NotNull();
            
        }


    }
}

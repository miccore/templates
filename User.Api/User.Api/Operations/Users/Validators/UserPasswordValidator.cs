using FluentValidation;
using Miccore.Net.webapi_template.User.Api.Operations.User.ViewModels;

namespace Miccore.Net.webapi_template.User.Api.Operations.User.Validator{
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

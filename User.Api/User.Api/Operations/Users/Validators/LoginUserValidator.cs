using FluentValidation;
using Miccore.Net.webapi_template.User.Api.Operations.User.ViewModels;

namespace Miccore.Net.webapi_template.User.Api.Operations.User.Validator{
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

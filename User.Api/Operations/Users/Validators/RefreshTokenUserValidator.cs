using FluentValidation;
using  Miccore.Net.webapi_template.User.Api.Operations.User.ViewModels;

namespace  Miccore.Net.webapi_template.User.Api.Operations.User.Validator{
    public class RefreshTokenUserValidator : AbstractValidator<RefreshTokenUserViewModel> {
        public RefreshTokenUserValidator() {

            RuleFor(user => user.RefreshToken)
                    .NotEmpty()
                    .NotNull();
            
        }


    }
}

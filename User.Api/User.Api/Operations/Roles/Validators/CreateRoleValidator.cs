using FluentValidation;
using  Miccore.Net.webapi_template.User.Api.Operations.Role.ViewModels;

namespace  Miccore.Net.webapi_template.User.Api.Operations.Role.Validator{
    public class CreateRoleValidator : AbstractValidator<CreateRoleViewModel> {
        public CreateRoleValidator() {

            RuleFor(role => role.Name)
                    .NotEmpty()
                    .NotNull();
            
            
        }


    }
}

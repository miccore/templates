using FluentValidation;
using  Miccore.Net.webapi_template.User.Api.Operations.Role.ViewModels;

namespace  Miccore.Net.webapi_template.User.Api.Operations.Role.Validator{
    public class UpdateRoleValidator : AbstractValidator<UpdateRoleViewModel> {
        public UpdateRoleValidator() {

                RuleFor(role => role.Id)
                    .NotEmpty()
                    .NotNull();
                RuleFor(role => role.Name)
                    .NotEmpty()
                    .NotNull();
            
            
        }


    }
}

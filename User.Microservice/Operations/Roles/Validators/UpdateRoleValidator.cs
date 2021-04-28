using FluentValidation;
using User.Microservice.Operations.Role.ViewModels;

namespace User.Microservice.Operations.Role.Validator{
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

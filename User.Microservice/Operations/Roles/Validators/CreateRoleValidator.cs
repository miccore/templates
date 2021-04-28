using FluentValidation;
using User.Microservice.Operations.Role.ViewModels;

namespace User.Microservice.Operations.Role.Validator{
    public class CreateRoleValidator : AbstractValidator<CreateRoleViewModel> {
        public CreateRoleValidator() {

            RuleFor(role => role.Name)
                    .NotEmpty()
                    .NotNull();
            
            
        }


    }
}

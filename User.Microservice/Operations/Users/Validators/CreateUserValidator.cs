using FluentValidation;
using User.Microservice.Operations.User.ViewModels;

namespace User.Microservice.Operations.User.Validator{
    public class CreateUserValidator : AbstractValidator<CreateUserViewModel> {
        public CreateUserValidator() {

            RuleFor(user => user.FirstName)
                    .NotEmpty()
                    .NotNull();
            
            RuleFor(user => user.Phone)
                    .NotEmpty()
                    .NotNull();
            
            RuleFor(user => user.Password)
                    .NotEmpty()
                    .NotNull();


            RuleFor(user => user.Password)
                    .NotEmpty()
                    .NotNull();
                    
            RuleFor(user => user.RoleId)
                    .NotEmpty()
                    .NotNull();
            
        }


    }
}

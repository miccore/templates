using FluentValidation;
using User.Microservice.Operations.User.ViewModels;

namespace User.Microservice.Operations.User.Validator{
    public class UpdateUserValidator : AbstractValidator<UpdateUserViewModel> {
        public UpdateUserValidator() {

                RuleFor(user => user.Id)
                    .NotEmpty()
                    .NotNull();
                RuleFor(user => user.FirstName)
                    .NotEmpty()
                    .NotNull();
            
                RuleFor(user => user.LastName)
                    .NotEmpty()
                    .NotNull();
            
                // RuleFor(user => user.Phone)
                //     .NotEmpty()
                //     .NotNull();
            
                RuleFor(user => user.Email)
                    .NotEmpty()
                    .NotNull();
            
                RuleFor(user => user.Address)
                    .NotEmpty()
                    .NotNull();

        }


    }
}

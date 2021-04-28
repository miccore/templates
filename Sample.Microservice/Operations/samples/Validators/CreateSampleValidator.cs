using FluentValidation;
using Sample.Microservice.Operations.Sample.ViewModels;

namespace Sample.Microservice.Operations.Sample.Validator{
    public class CreateSampleValidator : AbstractValidator<CreateSampleViewModel> {
        public CreateSampleValidator() {

            RuleFor(sample => sample.Name)
                    .NotEmpty()
                    .NotNull();
            
            RuleFor(sample => sample.City)
                    .NotEmpty()
                    .NotNull();
            
            RuleFor(sample => sample.Contact)
                    .NotEmpty()
                    .NotNull();
            
            RuleFor(sample => sample.Email)
                    .NotEmpty()
                    .NotNull();
            
        }


    }
}

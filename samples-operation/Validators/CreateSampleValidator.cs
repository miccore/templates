using FluentValidation;
using Miccore.Net.webapi_template.Sample.Api.Operations.Sample.ViewModels;

namespace Miccore.Net.webapi_template.Sample.Api.Operations.Sample.Validator{
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

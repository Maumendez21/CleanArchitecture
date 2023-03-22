using FluentValidation;

namespace CleanArchitecture.Application.Features.Directors.Commands.CreateDirector
{
    public class CreateDirectorCommandValidator : AbstractValidator<CreateDirectorCommand>
    {
        public CreateDirectorCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{Name} no puede ser nulo");
            
            RuleFor(p => p.LastName)
                .NotEmpty().WithMessage("{LastName} no puede ser nulo");
        }
    }
}

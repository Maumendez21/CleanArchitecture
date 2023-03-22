using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Features.Streamers.Commands
{
    public class CreateStreamerCommandValidator : AbstractValidator<CreateStreamerCommand>
    {
        public CreateStreamerCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{Name} no puede estar vacio")
                .NotNull()
                .MaximumLength(59).WithMessage("{Name} no puede exeder los 50 caracteres");

            RuleFor(p => p.Url).NotEmpty();
        }
    }
}

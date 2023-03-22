using FluentValidation;
namespace CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer
{
    public class UpdateStreamerCommandValidator : AbstractValidator<UpdateStreamerCommand>
    {
        public UpdateStreamerCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("No perimte nullos");
            RuleFor(x => x.Name).NotEmpty().WithMessage("No permite vacios").NotNull();
        }
    }
}

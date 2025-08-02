
using FluentValidation;

namespace Application.Features.ParkingSession.Commands.UpdateSessionCommands
{
    public class UpdateSessionCommandValidator : AbstractValidator<UpdateSessionCommand>
    {
        public UpdateSessionCommandValidator()
        {
             RuleFor(x => x.SlotId)
                .NotEmpty().WithMessage("El ID del slot es obligatorio.")
                .Must(slotId => slotId != Guid.Empty).WithMessage("El ID del slot no puede ser un GUID vacío.");

        }   
    }
}
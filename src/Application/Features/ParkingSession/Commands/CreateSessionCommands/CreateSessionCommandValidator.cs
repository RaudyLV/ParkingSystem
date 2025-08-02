

using Application.Features.ParkingSessions.Commands.CreateSessionCommands;
using FluentValidation;

namespace Application.Features.ParkingSession.Commands.CreateSessionCommands
{
    public class CreateSessionCommandValidator : AbstractValidator<CreateSessionCommand>
    {
        public CreateSessionCommandValidator()
        {
            RuleFor(x => x.SlotId)
                .NotEmpty().WithMessage("El ID del slot es obligatorio.")
                .Must(slotId => slotId != Guid.Empty).WithMessage("El ID del slot no puede ser un GUID vacío.");

            RuleFor(x => x.VehicleInfo)
                .NotEmpty().WithMessage("La información del vehículo es obligatoria.")
                .MaximumLength(100).WithMessage("La información del vehículo no puede exceder los 100 caracteres.");

            RuleFor(x => x.VehiclePlate)
                .NotEmpty().WithMessage("La matrícula del vehículo es obligatoria.")
                .Matches(@"^[A-Z0-9]{1,7}$").WithMessage("La matrícula del vehículo no es válida.")
                .MaximumLength(7).WithMessage("La matrícula del vehículo no puede exceder los 7 caracteres.");
        }
    }
}
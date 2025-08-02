

using FluentValidation;

namespace Application.Features.ParkingSlots.Commands.CreateSlotsCommand
{
    public class CreateSlotCommandValidator : AbstractValidator<CreateSlotCommand>
    {
        public CreateSlotCommandValidator()
        {
            RuleFor(x => x.LocationCode)
                .NotEmpty().WithMessage("El código de ubicación es obligatorio.");

            RuleFor(x => x.VehicleType)
                .NotEmpty().WithMessage("El tipo de vehículo es obligatorio.").MaximumLength(50).WithMessage("El tipo de vehículo no puede exceder los 50 caracteres.");
        }
    }
}
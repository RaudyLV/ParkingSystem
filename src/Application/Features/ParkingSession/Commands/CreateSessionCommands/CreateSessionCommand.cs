

using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using MediatR;

namespace Application.Features.ParkingSessions.Commands.CreateSessionCommands
{
    public class CreateSessionCommand : IRequest<Response<Guid>>
    {
        public Guid SlotId { get; set; }
        public string VehicleInfo { get; set; } = string.Empty;
        public string VehiclePlate { get; set; } = string.Empty;
    }

    public class CreateSessionCommandHandler : IRequestHandler<CreateSessionCommand, Response<Guid>>
    {
        private readonly IParkingSessionServices _parkingSessionServices;
        private readonly IParkingSlotService _parkingSlot;
        public CreateSessionCommandHandler(IParkingSessionServices parkingSessionServices, IParkingSlotService parkingSlot)
        {
            _parkingSessionServices = parkingSessionServices;
            _parkingSlot = parkingSlot;
        }

        public async Task<Response<Guid>> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
        {
            var slot = await _parkingSlot.GetParkingSlotByIdAsync(request.SlotId);
            if (slot.IsOccupied)
            {
                throw new SlotOccupiedException($"El espacio de estacionamiento {request.SlotId} no está disponible");
            }


            await _parkingSessionServices.StartSessionAsync(request.SlotId, request.VehicleInfo, request.VehiclePlate);

            slot.IsOccupied = true;
            await _parkingSlot.UpdateParkingSlotAsync(slot.Id, slot);

            return new Response<Guid>(request.SlotId, $"Sesión de estacionamiento iniciada en slot {request.SlotId}");
        }
    }
}
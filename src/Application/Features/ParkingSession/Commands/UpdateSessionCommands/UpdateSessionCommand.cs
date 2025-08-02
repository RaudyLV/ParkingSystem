using Application.Interfaces;
using Application.Wrappers;
using MediatR;


public class UpdateSessionCommand : IRequest<Response<Guid>>
{
    public Guid SessionId { get; set; }
    public Guid SlotId { get; set; }
}

public class UpdateSessionCommandHandler : IRequestHandler<UpdateSessionCommand, Response<Guid>>
{
    private readonly IParkingSessionServices _parkingSessionServices;
    private readonly IParkingSlotService _parkingSlotService;
    public UpdateSessionCommandHandler(IParkingSessionServices parkingSessionServices, IParkingSlotService parkingSlotService)
    {
        _parkingSessionServices = parkingSessionServices;
        _parkingSlotService = parkingSlotService;
    }

    public async Task<Response<Guid>> Handle(UpdateSessionCommand request, CancellationToken cancellationToken)
    {
        var slot = await _parkingSlotService.GetParkingSlotByIdAsync(request.SlotId);

        await _parkingSessionServices.EndSessionAsync(request.SessionId);

        slot.IsOccupied = false;
        await _parkingSlotService.UpdateParkingSlotAsync(request.SlotId, slot);

        return new Response<Guid>(request.SlotId, $"Salida registrada de slot {request.SlotId} correctamente."); 
    }
}

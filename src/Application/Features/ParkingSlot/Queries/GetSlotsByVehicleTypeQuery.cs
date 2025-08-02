

using Application.Dtos;
using Application.Interfaces;
using Application.Wrappers;
using MediatR;

namespace Application.Features.ParkingSlot.Queries
{
    public class GetSlotsByVehicleTypeQuery : IRequest<Response<List<ParkingSlotDto>>>
    {
        public string VehicleType { get; set; } = string.Empty;
    }

    public class GetSlotsByVehicleTypeQueryHandler : IRequestHandler<GetSlotsByVehicleTypeQuery, Response<List<ParkingSlotDto>>>
    {
        private readonly IParkingSlotService _parkingSlotService;

        public GetSlotsByVehicleTypeQueryHandler(IParkingSlotService parkingSlotService)
        {
            _parkingSlotService = parkingSlotService;
        }

        public async Task<Response<List<ParkingSlotDto>>> Handle(GetSlotsByVehicleTypeQuery request, CancellationToken cancellationToken)
        {
            var slots = await _parkingSlotService.GetParkingSlotByVehicleTypeAsync(request.VehicleType);
            
            return new Response<List<ParkingSlotDto>>(slots);
        }
    }
}
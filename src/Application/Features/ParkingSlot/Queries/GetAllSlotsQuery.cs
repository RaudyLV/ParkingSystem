

using Application.Dtos;
using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using MediatR;

namespace Application.Features.ParkingSlot.Queries
{
    public class GetAllSlotsQuery : IRequest<Response<List<ParkingSlotDto>>>
    {

    }
    
    public class GetAllSlotsQueryHandler : IRequestHandler<GetAllSlotsQuery, Response<List<ParkingSlotDto>>>
    {
        private readonly IParkingSlotService _parkingSlotService;
        private readonly IMapper _mapper;
        public GetAllSlotsQueryHandler(IParkingSlotService parkingSlotService, IMapper mapper)
        {
            _parkingSlotService = parkingSlotService;
            _mapper = mapper;
        }

        public async Task<Response<List<ParkingSlotDto>>> Handle(GetAllSlotsQuery request, CancellationToken cancellationToken)
        {
            var slots = await _parkingSlotService.GetAllParkingSlotsAsync();
            if (slots == null || !slots.Any())
            {
                throw new NotFoundException("No hay ningun espacio de estacionamiento existente.");
            }

            return new Response<List<ParkingSlotDto>>(slots);
        }
    }
}
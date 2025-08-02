

using Application.Dtos;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using MediatR;

namespace Application.Features.ParkingSlots.Commands.CreateSlotsCommand
{
    public class CreateSlotCommand : IRequest<Response<Guid>>
    {
        public string LocationCode { get; set; } = string.Empty;
        public string VehicleType { get; set; } = string.Empty;
    }

    public class CreateSlotCommandHandler : IRequestHandler<CreateSlotCommand, Response<Guid>>
    {
        private readonly IParkingSlotService _parkingSlotService;
        private readonly IMapper _mapper;
        public CreateSlotCommandHandler(IParkingSlotService parkingSlotService, IMapper mapper)
        {
            _parkingSlotService = parkingSlotService;
            _mapper = mapper;
        }

        public async Task<Response<Guid>> Handle(CreateSlotCommand request, CancellationToken cancellationToken)
        {
            var slotDto = _mapper.Map<ParkingSlotDto>(request);

            await _parkingSlotService.CreateParkingSlotAsync(slotDto);

            return new Response<Guid>("Parking slot creado exitosamente.");
        }
    }
}
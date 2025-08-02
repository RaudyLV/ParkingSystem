

using Application.Dtos;
using Application.Features.ParkingSessions.Commands.CreateSessionCommands;
using Application.Features.ParkingSlots.Commands.CreateSlotsCommand;
using AutoMapper;
using Domain.Entidades;

namespace Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<ParkingSlot, ParkingSlotDto>();
            CreateMap<ParkingSession, ParkingSessionDto>();

            CreateMap<CreateSlotCommand, ParkingSlotDto>();
            
            CreateMap<CreateSessionCommand, ParkingSessionDto>();
            CreateMap<UpdateSessionCommand, ParkingSessionDto>();
                
        }
    }
}
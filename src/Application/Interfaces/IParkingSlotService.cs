

using Application.Dtos;
using Domain.Entidades;

namespace Application.Interfaces
{
    public interface IParkingSlotService
    {
        //CRUD
        Task<ParkingSlotDto> GetParkingSlotByIdAsync(Guid id);
        Task<List<ParkingSlotDto>> GetAllParkingSlotsAsync();
        Task CreateParkingSlotAsync(ParkingSlotDto parkingSlot);
        Task UpdateParkingSlotAsync(Guid id, ParkingSlotDto parkingSlot);
        //--------------------------------------------------------------------------------

        Task<List<ParkingSlotDto>> GetParkingSlotByVehicleTypeAsync(string vehicleType);
    }
}
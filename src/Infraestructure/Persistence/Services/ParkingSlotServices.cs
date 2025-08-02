

using Application.Dtos;
using Application.Exceptions;
using Application.Interfaces;
using Application.Spec.ParkingSlots;
using Domain.Entidades;

namespace Infraestructure.Persistence.Services
{
    public class ParkingSlotServices : IParkingSlotService
    {
        private readonly IRepositoryAsync<ParkingSlot> _repository;

        public ParkingSlotServices(IRepositoryAsync<ParkingSlot> repository)
        {
            _repository = repository;
        }

        public async Task CreateParkingSlotAsync(ParkingSlotDto parkingSlot)
        {
            var parkingSlotEntity = new ParkingSlot
            {
                LocationCode = parkingSlot.LocationCode,
                VehicleType = parkingSlot.VehicleType,
                IsOccupied = false
            };

            await _repository.AddAsync(parkingSlotEntity);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateParkingSlotAsync(Guid id, ParkingSlotDto parkingSlot)
        {
            var existingParkingSlot = await _repository.GetByIdAsync(id);
            if (existingParkingSlot is null)
            {
                throw new NotFoundException($"No se encontró el espacio de estacionamiento con ID: {id}");
            }

            existingParkingSlot.IsOccupied = parkingSlot.IsOccupied;
            
            await _repository.UpdateAsync(existingParkingSlot);
            await _repository.SaveChangesAsync();
        }

        public async Task<List<ParkingSlotDto>> GetAllParkingSlotsAsync()
        {
            return await _repository.ListAsync(new PagedSlotSpec());
        }

        public async Task<ParkingSlotDto> GetParkingSlotByIdAsync(Guid id)
        {
            var parkingSlot = await _repository.GetByIdAsync(id);
            if (parkingSlot is null)
            {
                throw new NotFoundException($"No se encontró el espacio de estacionamiento {id}");
            }

            return new ParkingSlotDto
            {
                Id = parkingSlot.Id,
                VehicleType = parkingSlot.VehicleType,
                IsOccupied = parkingSlot.IsOccupied,
                HourlyRate = parkingSlot.HourlyRate
            };
        }

        public async Task<List<ParkingSlotDto>> GetParkingSlotByVehicleTypeAsync(string vehicleType)
        {
            var parkingSlots = await _repository.ListAsync(new GetParkingSlotByVehicleTypeSpec(vehicleType));
            if(!parkingSlots.Any())
            {
                throw new NoSlotAvailableException($"No hay espacios disponibles para el tipo de vehículo: {vehicleType}");
            }
            return parkingSlots;
        }
    }
}
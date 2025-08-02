
using Application.Dtos;
using Ardalis.Specification;
using Domain.Entidades;

namespace Application.Spec.ParkingSlots
{
    public class GetParkingSlotByVehicleTypeSpec : Specification<ParkingSlot, ParkingSlotDto>
    {
        public GetParkingSlotByVehicleTypeSpec(string vehicleType)
        {
            Query.Where(ps => ps.VehicleType == vehicleType)
            .Select(ps => new ParkingSlotDto
            {
                Id = ps.Id,
                VehicleType = ps.VehicleType,
                LocationCode = ps.LocationCode,
                IsOccupied = ps.IsOccupied,
                HourlyRate = ps.HourlyRate
            });
        }
    }
}
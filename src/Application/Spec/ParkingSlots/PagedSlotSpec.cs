
using Application.Dtos;
using Ardalis.Specification;
using Domain.Entidades;

namespace Application.Spec.ParkingSlots
{
    public class PagedSlotSpec : Specification<ParkingSlot, ParkingSlotDto>
    {
        public PagedSlotSpec()
        {
            Query.OrderBy(ps => ps.LocationCode)
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
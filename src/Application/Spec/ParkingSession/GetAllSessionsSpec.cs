

using Application.Dtos;
using Ardalis.Specification;
using Domain.Entidades;

namespace Application.Spec.ParkingSessions
{
    public class GetAllSessionsSpec : Specification<ParkingSession, ParkingSessionDto>
    {
        public GetAllSessionsSpec()
        {
            Query.Where(ps => ps.IsActive)
                .OrderByDescending(ps => ps.StartTime)
                .AsNoTracking()
                .Select(ps => new ParkingSessionDto
                {
                    Id = ps.Id,
                    ParkingSlotId = ps.ParkingSlotId,
                    VehiclePlate = ps.VehiclePlate,
                    VehicleType = ps.VehicleInfo,
                    TotalCost = ps.TotalCost,
                    StartTime = ps.StartTime,
                    EndTime = ps.EndTime,
                    IsActive = ps.IsActive
                });
        }
    }
}
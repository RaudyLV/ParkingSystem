

using Ardalis.Specification;
using Domain.Entidades;

namespace Application.Spec.ParkingSessions
{
    public class GetActiveSessionSpec : Specification<ParkingSession>
    {
        public GetActiveSessionSpec(Guid seccionId)
        {
            Query
                .Where(ps => ps.Id == seccionId && ps.IsActive)
                .OrderByDescending(ps => ps.StartTime);
        }
    }
}
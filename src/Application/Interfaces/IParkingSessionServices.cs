

using Application.Dtos;
using Domain.Entidades;

namespace Application.Interfaces
{
    public interface IParkingSessionServices
    {
        Task StartSessionAsync(Guid slotId, string vehicleInfo, string vehiclePlate);
        Task EndSessionAsync(Guid seccionId);
        Task<ParkingSession> GetSessionByIdAsync(Guid seccionId);
        Task<List<ParkingSessionDto>> GetActiveSessionsAsync();
        Task<ParkingSession> GetActiveSessionAsync(Guid seccionId);
        Task<ParkingSessionDto> GetSessionByVehiclePlateAsync(string vehiclePlate);
    }
}
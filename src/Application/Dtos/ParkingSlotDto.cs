

namespace Application.Dtos
{
    public class ParkingSlotDto
    {
        public Guid Id { get; set; }
        public string LocationCode { get; set; } = string.Empty;
        public string VehicleType { get; set; } = string.Empty;
        public bool IsOccupied { get; set; }
        public decimal HourlyRate { get; set; }
    }
}
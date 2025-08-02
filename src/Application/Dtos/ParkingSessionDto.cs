

namespace Application.Dtos
{
    public class ParkingSessionDto
    {
        public Guid Id { get; set; }
        public Guid ParkingSlotId { get; set; }
        public string VehiclePlate { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal TotalCost { get; set; }
        public bool IsActive { get; set; }
        public string VehicleType { get; set; } = string.Empty; 

    }
}
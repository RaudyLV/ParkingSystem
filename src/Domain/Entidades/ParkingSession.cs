

namespace Domain.Entidades
{
    public class ParkingSession
    {
        public Guid Id { get; set; }
        public Guid ParkingSlotId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal TotalCost { get; set; } 
        public string VehiclePlate { get; set; } = string.Empty;
        public string VehicleInfo { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public ParkingSlot ParkingSlot { get; set; } = null!;
    }
}
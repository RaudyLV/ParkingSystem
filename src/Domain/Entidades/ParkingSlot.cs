

namespace Domain.Entidades
{
    public class ParkingSlot
    {
        public Guid Id { get; set; }
        public string LocationCode { get; set; } = string.Empty;
        public string VehicleType { get; set; } = string.Empty; //Camiones, Autos, Motos, etc.
        public bool IsOccupied { get; set; }
        public decimal HourlyRate { get; set; }
        
    }
}
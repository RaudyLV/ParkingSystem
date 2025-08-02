

using Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Persistence.Configurations
{
    public class ParkingSlotConfig : IEntityTypeConfiguration<ParkingSlot>
    {
        public void Configure(EntityTypeBuilder<ParkingSlot> builder)
        {
            builder.ToTable("ParkingSlots");
            builder.HasKey(ps => ps.Id);
            builder.Property(ps => ps.VehicleType).IsRequired();
            builder.Property(ps => ps.LocationCode).IsRequired();
            builder.Property(ps => ps.IsOccupied).IsRequired();
            builder.Property(ps => ps.HourlyRate).HasDefaultValueSql("100.00");
        }
    }
    
}
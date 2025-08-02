
using Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Persistence.Configurations
{
    public class ParkingSessionConfig : IEntityTypeConfiguration<ParkingSession>
    {
        public void Configure(EntityTypeBuilder<ParkingSession> builder)
        {
            builder.HasKey(ps => ps.Id);
            builder.Property(ps => ps.VehiclePlate).IsRequired();
            builder.Property(ps => ps.StartTime).IsRequired();
            builder.Property(ps => ps.EndTime)
                   .IsRequired(false); // EndTime puede ser nulo si la sesión aún está activa
            builder.Property(ps => ps.ParkingSlotId).IsRequired();
            builder.Property(ps => ps.TotalCost).HasDefaultValueSql("0.00");
            builder.Property(ps => ps.VehicleInfo).HasMaxLength(100).IsRequired();
            builder.Property(ps => ps.IsActive).HasDefaultValue(true);

            builder.HasOne(ps => ps.ParkingSlot)
                   .WithMany()
                   .HasForeignKey(ps => ps.ParkingSlotId);
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetShop.Domain.Entities;

namespace PetShop.Infrastructure.Configurations;

public class LocationEntityTypeConfiguration: IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder
            .HasOne(l => l.City)
            .WithMany(c => c.Locations)
            .HasForeignKey(l => l.CityId);
    }
}

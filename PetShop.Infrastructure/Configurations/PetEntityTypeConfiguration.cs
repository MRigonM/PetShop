using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetShop.Domain.Entities;

namespace PetShop.Infrastructure.Configurations;

public class PetEntityTypeConfiguration: IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder
            .HasOne(p => p.Breed)
            .WithMany(b => b.Pets)
            .HasForeignKey(p => p.BreedId);

        builder
            .HasOne(p => p.Location)
            .WithMany(l => l.Pets)
            .HasForeignKey(p => p.LocationId);

        builder
            .HasIndex(p => p.Status);
    }
}

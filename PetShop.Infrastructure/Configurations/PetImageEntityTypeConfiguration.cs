﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetShop.Domain.Entities;

namespace PetShop.Infrastructure.Configurations;

public class PetImageEntityTypeConfiguration: IEntityTypeConfiguration<PetImage>
{
    public void Configure(EntityTypeBuilder<PetImage> builder)
    {
        builder
            .HasOne(pi => pi.Pet)
            .WithMany(p => p.PetImages)
            .HasForeignKey(pi => pi.PetId);
    }
}
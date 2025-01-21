using Microsoft.EntityFrameworkCore;
using PetShop.Domain.Entities;
using PetShop.Domain.Enums;
using PetShop.Domain.Helpers;
using PetShop.Domain.Interfaces;
using PetShop.Infrastructure.Common;
using PetShop.Infrastructure.Data;

namespace PetShop.Infrastructure.Repositories;

public class PetRepository : GenericRepository<Pet, int>, IPetRepository
{
    public PetRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<(List<Pet> Pets, int TotalCount)> GetAvailablePetsWithDetailsAsync(QueryParams queryParams)
    {
        var query = _dbSet
            .AsNoTracking()
            .Select(p => new Pet
            {
                Id = p.Id,
                Name = p.Name,
                AgeYears = p.AgeYears,
                About = p.About,
                Status = p.Status,
                BreedId = p.BreedId,
                LocationId = p.LocationId,
                PostedByUserId = p.PostedByUserId,
                Breed = new Breed
                {
                    Id = p.Breed.Id,
                    Name = p.Breed.Name,
                    SpeciesId = p.Breed.SpeciesId,
                    Species = new Species
                    {
                        Id = p.Breed.Species.Id,
                        Name = p.Breed.Species.Name,
                    }
                },
                Location = new Location
                {
                    Id = p.Location.Id,
                    Address = p.Location.Address,
                    PostalCode = p.Location.PostalCode,
                    CityId = p.Location.CityId,
                    City = new City
                    {
                        Id = p.Location.City.Id,
                        Name = p.Location.City.Name,
                        CountryId = p.Location.City.CountryId,
                        Country = new Country
                        {
                            Id = p.Location.City.Country.Id,
                            Name = p.Location.City.Country.Name
                        }
                    }
                },
                PetImages = p.PetImages.Select(pi => new PetImage
                {
                    Id = pi.Id,
                    ImageUrl = pi.ImageUrl,
                    IsPrimary = pi.IsPrimary,
                    PetId = pi.PetId
                }).ToList()
            })
            .Where(p => p.Status == PetStatus.Available)
            .AsQueryable();


        query = query.ApplyQueryParams(queryParams);

        var totalCount = await query.CountAsync();
        query = query
            .Skip((queryParams.PageNumber - 1) * queryParams.PageSize)
            .Take(queryParams.PageSize);

        var generatedSql = query.ToQueryString(); // For debugging only!
        var result = await query.ToListAsync();

        return (Pets: result, TotalCount: totalCount);
    }

    public async Task<IEnumerable<Pet>> GetByUserIdAsync(string userId)
    {
        return await _dbSet
            .AsNoTracking()
            .Where(pet => pet.PostedByUserId == userId)
            .Include(pet => pet.Location)
            .ThenInclude(location => location.City)
            .Include(pet => pet.Breed)
            .Include(pet => pet.PetImages)
            .ToListAsync();
    }

    public async Task<Pet?> GetPetByIdWithRelatedEntitiesAsync(int id)
    {
        return await _dbSet
            .Include(p => p.PetImages)
            .Include(p => p.Breed)
            .ThenInclude(b => b.Species)
            .Include(p => p.Location)
            .ThenInclude(p => p.City)
            .ThenInclude(p => p.Country)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Pet?> GetPetByIdWithRelatedEntitiesAsync(int id, string userId)
    {
        return await _dbSet
            .Include(p => p.PetImages)
            .Include(p => p.Breed)
            .ThenInclude(b => b.Species)
            .Include(p => p.Location)
            .ThenInclude(p => p.City)
            .ThenInclude(p => p.Country)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Pet>> GetPetsByUserIdWithUserDetailsAsync(string userId)
    {
        return await _dbSet
            .AsNoTracking()
            .AsSplitQuery()
            .Where(pet => pet.PostedByUserId == userId)
            .Include(p => p.Location)
            .ThenInclude(p => p.City)
            .ThenInclude(p => p.Country)
            .Include(pet => pet.Breed)
            .Include(pet => pet.PetImages)
            .ToListAsync();
    }
}
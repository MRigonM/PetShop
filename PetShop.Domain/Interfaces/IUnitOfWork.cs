﻿namespace PetShop.Domain.Interfaces;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}
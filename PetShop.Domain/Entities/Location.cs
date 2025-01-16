﻿using PetShop.Domain.Interfaces;

namespace PetShop.Domain.Entities;

public class Location : IEntity<int>
{
    public int Id { get; set; }
    public int CityId { get; set; }
    public City City { get; set; }
    public string Address { get; set; }
    public string PostalCode { get; set; }

}
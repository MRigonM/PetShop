﻿namespace PetShop.Application.Models.Identity;

public class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}
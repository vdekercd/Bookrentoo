﻿namespace DamienVDK.Bookrentoo.OrganizationApp.Models.Forms;

public class RegisterRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
}
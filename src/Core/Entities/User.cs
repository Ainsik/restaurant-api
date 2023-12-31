﻿namespace Core.Entities;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string CreatedDate { get; set; } = DateTime.Now.ToString("dd/MM/yyyy");
    public string PasswordHash { get; set; }
    public int RoleId { get; set; } = 1;
    public virtual Role Role { get; set; }
}
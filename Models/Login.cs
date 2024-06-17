using Microsoft.EntityFrameworkCore;
using VirtualHoftalon_Server.Enums;

namespace VirtualHoftalon_Server.Models;

public class Login
{
    public int Id { get; set; }
    public string Username { get; set;}
    public string Password { get; set; }
    public Roles Role { get; set;}
    public string  RefreshToken { get; set;}

}
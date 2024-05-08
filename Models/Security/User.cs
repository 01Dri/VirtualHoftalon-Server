namespace VirtualHoftalon_Server.Models.Security;

public class User
{
    public int? Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}
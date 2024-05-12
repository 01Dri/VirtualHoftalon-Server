using VirtualHoftalon_Server.Models;
using VirtualHoftalon_Server.Repositories.Interfaces;

namespace VirtualHoftalon_Server.Repositories;

public class LoginRepository : ILoginRepository
{
    private readonly ModelsContext _context;

    public LoginRepository(ModelsContext context)
    {
        _context = context;
    }
    public Login Save(Login login)
    {
        _context.Logins.Add(login);
        _context.SaveChanges();
        return login;
    }

    public Login GetLoginByUsernameAndPassword(string username, string password)
    {
        return _context.Logins
            .Where(l => l.Username == username && l.Password == password)
            .FirstOrDefault();
    }

    public Login GetLoginByUsername(string username)
    {
        return _context.Logins.Where(l => l.Username == username).FirstOrDefault();
    }
}
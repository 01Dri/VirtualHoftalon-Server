using Microsoft.Data.SqlClient;
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
        using var transaction = _context.Database.BeginTransaction();
        try
        {

            _context.Logins.Add(login);
            _context.SaveChanges();
            return login;
        }
        catch (SqlException e)
        {
            transaction.Rollback();
            throw new Exception(e.Message);
        }
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
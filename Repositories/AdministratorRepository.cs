using Microsoft.EntityFrameworkCore;
using VirtualHoftalon_Server.Exceptions;
using VirtualHoftalon_Server.Models;
using VirtualHoftalon_Server.Repositories.Interfaces;

namespace VirtualHoftalon_Server.Repositories;

public class AdministratorRepository : IAdministratorRepository
{

    private readonly ModelsContext _context;

    public AdministratorRepository(ModelsContext context)
    {
        _context = context;
    }

    public Administrator? GetAdministratorById(int id)
    {
        return _context.Administrators.Where(a => a.Id == id).FirstOrDefault();
    }

    public Administrator SaveAdministrator(Administrator administrator)
    {
        using var transaction = _context.Database.BeginTransaction();
        try
        {
            _context.Administrators.Add(administrator);
            _context.SaveChanges();
            transaction.Commit();
            return administrator;
        }
        catch (Exception e)
        {
            transaction.Rollback();
            throw new FailedToSaveLoginException("Failed to save Login");
        } 
    }

    public IEnumerable<Administrator> GetAll()
    {
        return _context.Administrators.Include(a => a.Login).ToList();
    }

    public Administrator GetByLoginId(int id)
    {
        return _context.Administrators.Include(a => a.Login)
            .Where(adm => adm.Login.Id == id).FirstOrDefault();
    }

    public Administrator UpdateAdministrator(Administrator administrator)
    {
        using var transaction = _context.Database.BeginTransaction();
        try
        {
            var login = _context.Logins.Add(administrator.Login);
            _context.SaveChanges();
            administrator.Login = login.Entity;
            administrator.LoginId = login.Entity.Id;
            _context.Administrators.Update(administrator);
            _context.SaveChanges();
            transaction.Commit();
            return administrator;
        }
        catch (Exception e)
        {
            transaction.Rollback();
            throw new FailedToSaveLoginException("Failed to save Login" + e.Message);
        } 
    }


    public void Delete(Administrator administrator)
    {
        _context.Administrators.Remove(administrator);
    }
}
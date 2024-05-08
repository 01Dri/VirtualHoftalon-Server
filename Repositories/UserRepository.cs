using VirtualHoftalon_Server.Models;
using VirtualHoftalon_Server.Models.Security;
using VirtualHoftalon_Server.Repositories.Interfaces;

namespace VirtualHoftalon_Server.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ModelsContext _modelsContext;

    public UserRepository(ModelsContext modelsContext)
    {
        _modelsContext = modelsContext;
    }

    public User GetUserById(int? id)
    {
        throw new NotImplementedException();
    }

    public User SaveUser(User? user)
    {
        _modelsContext.Users.Add(user);
        _modelsContext.SaveChanges();
        return user;
    }

    public IEnumerable<User> GetAll()
    {
        throw new NotImplementedException();
    }

    public User UpdateUser(User? UserById)
    {
        throw new NotImplementedException();
    }

    public void Delete(User? UserById)
    {
        throw new NotImplementedException();
    }
    
}
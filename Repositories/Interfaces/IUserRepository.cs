using VirtualHoftalon_Server.Models.Security;

namespace VirtualHoftalon_Server.Repositories.Interfaces;

public interface IUserRepository
{
    
    User GetUserById(int? id);
    User SaveUser(User? User);
    IEnumerable<User> GetAll();
    User UpdateUser(User? UserById);
    void Delete(User? UserById);


}
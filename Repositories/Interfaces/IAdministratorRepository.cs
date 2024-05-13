
using VirtualHoftalon_Server.Models;

namespace VirtualHoftalon_Server.Repositories.Interfaces;

public interface IAdministratorRepository
{
    
    Administrator? GetAdministratorById(int id);

    Administrator SaveAdministrator(Administrator administrator);

    IEnumerable<Administrator> GetAll();
    
    Administrator UpdateAdministrator(Administrator administrator);
    void Delete(Administrator administrator);

    
}
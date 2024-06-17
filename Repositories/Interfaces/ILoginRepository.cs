using VirtualHoftalon_Server.Models;

namespace VirtualHoftalon_Server.Repositories.Interfaces;

public interface ILoginRepository
{
    Login Save(Login login);
    Login GetLoginByUsernameAndPassword(string Username, string Password);
    Login GetLoginByUsername(string Username);
    Login Update(Login login);

}
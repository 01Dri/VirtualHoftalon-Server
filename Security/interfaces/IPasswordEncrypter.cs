namespace VirtualHoftalon_Server.Security.interfaces;

public interface IPasswordEncrypter
{
    string Encrypt(string password);
    string Decrypt(string password);
}
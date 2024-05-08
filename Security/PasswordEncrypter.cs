using System.Security.Cryptography;
using System.Text;
using VirtualHoftalon_Server.Security.interfaces;

namespace VirtualHoftalon_Server.Security;

public class PasswordEncrypter : IPasswordEncrypter
{
    private const string KeyPassword = Settings.KeyPassword;

    public string Encrypt(string password)
    {
        byte[] clearBytes = Encoding.Unicode.GetBytes(password);
        using (Aes aes = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(KeyPassword, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            aes.Key = pdb.GetBytes(32);
            aes.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }

                password = Convert.ToBase64String(ms.ToArray());
            }
        }

        return password;
    }

    public string Decrypt(string password)
    {
        byte[] cipherBytes = Convert.FromBase64String(password);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(KeyPassword, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                password = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
 
        return password;
    }
    
}
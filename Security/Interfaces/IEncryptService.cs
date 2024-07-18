namespace Security.Interfaces;

public interface IEncryptService
{
    string Encrypt(string password);
    
    bool VerifyPassword(string password, string hash);
}
namespace DotNetCore.Security
{
    public interface ICryptographyService
    {
        string Decrypt(string value);

        string Decrypt(string value, string salt);

        string Encrypt(string value);

        string Encrypt(string value, string salt);
    }
}

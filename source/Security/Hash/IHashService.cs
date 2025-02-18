namespace DotNetCore.Security;

public interface IHashService
{
    string Create(string value, string salt);

    bool Validate(string hash, string value, string salt);
}

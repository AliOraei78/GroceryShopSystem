// Core/Security/IPasswordHasher.cs (abstraction)
public interface IPasswordHasher
{
    string Hash(string password);
    bool Verify(string password, string hash);
}
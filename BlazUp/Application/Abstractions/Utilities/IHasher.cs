namespace Application.Abstractions.Utilities;

public interface IHasher {
    string HashPassword(string password);
    bool VerifyPassword(string password, string hashedPassword);
}

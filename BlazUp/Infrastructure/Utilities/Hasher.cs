using Application.Abstractions.Utilities;

namespace Infrastructure.Utilities;

public class Hasher : IHasher {
    public Hasher() { }

    public string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);
    public bool VerifyPassword(string password, string hashedPassword) => BCrypt.Net.BCrypt.Verify(password, hashedPassword);
}

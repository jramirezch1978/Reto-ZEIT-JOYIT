using BCrypt.Net;

namespace CleanArchitecture.Application.Tests;

public class BCryptTest
{
    public static void TestHashCompatibility()
    {
        string password = "myPassword123";
        
        // Generar hash con el método antiguo
        string oldHash = BCrypt.Net.BCrypt.HashPassword(password);
        
        // Generar hash con el método nuevo
        string newHash = BCrypt.Net.BCrypt.EnhancedHashPassword(password);
        
        // Verificar que ambos hashes funcionan con el método Verify
        bool oldHashValid = BCrypt.Net.BCrypt.Verify(password, oldHash);
        bool newHashValid = BCrypt.Net.BCrypt.Verify(password, newHash);
        
        Console.WriteLine($"Old hash: {oldHash}");
        Console.WriteLine($"New hash: {newHash}");
        Console.WriteLine($"Old hash verification: {oldHashValid}");
        Console.WriteLine($"New hash verification: {newHashValid}");
    }
} 
using CleanArchitecture.Application.Tests;

namespace CleanArchitecture.Application;

public class Program
{
    public static void Main(string[] args)
    {
        BCryptTest.TestHashCompatibility();
    }
} 
// Test file for modern C# features (C# 10-13)

// Global using
global using System.Collections.Generic;

// File-scoped namespace
namespace MyApp.Services;

// Record types
public record PersonRecord(string FirstName, string LastName);
public record class AnotherRecord(int Id, string Name);
public record struct PointRecord(double X, double Y);

// Required members
public class Config
{
    public required string ConnectionString { get; init; }
    public required int MaxRetries { get; init; }
}

// Primary constructors
public class UserService(ILogger logger, IRepository repo)
{
    public void DoWork()
    {
        logger.Log("Working...");
        var items = repo.GetAll();
    }
}

// Init-only setters
public class Settings
{
    public string Name { get; init; }
    public int Value { get; set; }
    public bool IsEnabled { get; }
}

// File-local type
file class InternalHelper
{
    public static void Help() { }
}

// Pattern matching (modern)
public static class Patterns
{
    public static string Describe(object obj) => obj switch
    {
        int i when i > 0 => "positive",
        int i and < 0 => "negative",
        string { Length: > 10 } s => $"long string: {s}",
        int[] { Length: > 0 } arr => $"non-empty array of {arr.Length}",
        [var first, .., var last] => $"list from {first} to {last}",
        null => "null",
        _ => "unknown"
    };
}

// Raw string literals
public class RawStrings
{
    public string Json = """
        {
            "name": "test",
            "value": 42
        }
        """;

    public string Interpolated = $"""
        Hello {DateTime.Now}
        """;
}

// Generic math / static abstract interface members
public interface IAddable<T> where T : IAddable<T>
{
    static abstract T operator +(T left, T right);
}

// Test file for modern C# features (C# 10-13)


// Type aliases (C# 12)
using Point = (int X, int Y);
using Measurement = (double Value, string Unit);



// Global using
global using System.Collections.Generic;
// Attributes on various targets
[assembly: System.Reflection.AssemblyVersion("1.0.0.0")]
[module: System.CLSCompliant(false)]

// File-scoped namespace
namespace MyApp.Services;

namespace Lol {
    public class Test
    {
        public void Method() { }
    }
}

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
    static virtual T Zero => default;
}

// Checked user-defined operators
public readonly struct SafeInt
{
    public readonly int Value;
    public SafeInt(int value) => Value = value;

    public static SafeInt operator +(SafeInt a, SafeInt b) => new(a.Value + b.Value);
    public static SafeInt operator checked +(SafeInt a, SafeInt b) => new(checked(a.Value + b.Value));

    public static explicit operator int(SafeInt s) => s.Value;
    public static explicit operator checked int(SafeInt s) => checked(s.Value);
}

// Nullable reference types
#nullable enable
public class NullableExample
{
    public string NonNullable { get; set; } = "";
    public string? Nullable { get; set; }

    public string GetValue(string? input)
    {
        return input ?? NonNullable;
    }

    public void NullForgiving()
    {
        string? maybe = null;
        string definitely = maybe!;
    }
}
#nullable restore

// Tuples and deconstruction
public class TupleExamples
{
    public (int X, int Y) GetPoint() => (10, 20);

    public void Deconstruct()
    {
        var (x, y) = GetPoint();
        (int a, int b) = (1, 2);
        var (_, second) = GetPoint(); // discard
    }
}

// Indices and ranges
public class IndicesAndRanges
{
    public void Examples()
    {
        int[] arr = [1, 2, 3, 4, 5];
        int last = arr[^1];
        int[] slice = arr[1..^1];
        int[] fromStart = arr[..3];
        int[] toEnd = arr[2..];
        Range range = 1..4;
        Index idx = ^2;
    }
}

// Collection expressions (C# 12)
public class CollectionExpressions
{
    public int[] Numbers = [1, 2, 3, 4, 5];
    public List<string> Names = ["Alice", "Bob", "Charlie"];
    public Span<int> SpanValues = [10, 20, 30];

    public void Spread()
    {
        int[] first = [1, 2, 3];
        int[] second = [4, 5, 6];
        int[] combined = [.. first, .. second, 7, 8];
    }
}

// Inline arrays (C# 12)
[System.Runtime.CompilerServices.InlineArray(10)]
public struct InlineBuffer
{
    private int _element;
}

// Lambda improvements (C# 10+)
public class LambdaFeatures
{
    // Natural type
    var parse = (string s) => int.Parse(s);

    // Explicit return type
    var choose = object (bool b) => b ? 1 : "hello";

    // Attributes on lambdas
    var validate = [Obsolete("Use ValidateV2")] (int x) => x > 0;

    // Default parameter values (C# 12)
    var greet = (string name = "World") => $"Hello, {name}!";

    // Params in lambdas (C# 13)
    var sum = (params int[] numbers) => numbers.Sum();

    // Static lambdas
    Func<int, int> doubler = static x => x * 2;
}

// Default interface methods
public interface IVersioned
{
    string Version => "1.0";
    void PrintVersion() => Console.WriteLine(Version);
    static void StaticMethod() => Console.WriteLine("Static interface method");
}

// Target-typed new
public class TargetTypedNew
{
    private List<int> _items = new();
    private Dictionary<string, List<int>> _map = new();

    public void Method()
    {
        StringBuilder sb = new();
        Point p = new(10, 20);
    }
}

// With expressions (records and structs)
public record Coordinate(double Lat, double Lon);
public readonly record struct Temperature(double Celsius)
{
    public double Fahrenheit => Celsius * 9 / 5 + 32;
}

public class WithExpressions
{
    public void Examples()
    {
        var coord = new Coordinate(47.6, -122.3);
        var moved = coord with { Lat = 48.0 };

        var temp = new Temperature(100);
        var cooler = temp with { Celsius = 20 };
    }
}

// Extended property patterns (C# 10)
public class Address { public City City { get; set; } }
public class City { public string Name { get; set; } public int Population { get; set; } }

public class ExtendedPropertyPatterns
{
    public bool IsLargeSeattleAddress(Address addr) => addr is
    {
        City.Name: "Seattle",
        City.Population: > 500_000
    };
}

// Pattern matching: relational, logical, type, declaration, var, positional
public class AdvancedPatterns
{
    public string Classify(int value) => value switch
    {
        < 0 => "negative",
        0 => "zero",
        > 0 and < 10 => "small",
        >= 10 and <= 100 => "medium",
        > 100 or int.MaxValue => "large",
        _ => "other"
    };

    public bool IsLetter(char c) => c is (>= 'a' and <= 'z') or (>= 'A' and <= 'Z');

    public string TypePattern(object o) => o switch
    {
        int => "integer",
        string s when s.Length > 0 => $"non-empty string: {s}",
        not null => "something",
        _ => "null"
    };
}

// Async streams
public class AsyncStreams
{
    public async IAsyncEnumerable<int> GenerateAsync()
    {
        for (int i = 0; i < 10; i++)
        {
            await Task.Delay(100);
            yield return i;
        }
    }

    public async Task ConsumeAsync()
    {
        await foreach (var item in GenerateAsync())
        {
            Console.WriteLine(item);
        }
    }
}

// Local functions (static and regular)
public class LocalFunctions
{
    public int Fibonacci(int n)
    {
        return Fib(n);

        static int Fib(int n) => n <= 1 ? n : Fib(n - 1) + Fib(n - 2);
    }

    public IEnumerable<int> Filter(IEnumerable<int> source, int min)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        return Iterator();

        IEnumerable<int> Iterator()
        {
            foreach (var item in source)
                if (item >= min)
                    yield return item;
        }
    }
}

// Native integers and function pointers
public class UnsafeFeatures
{
    public nint NativeInt = 42;
    public nuint NativeUInt = 100;

    public unsafe void FunctionPointers()
    {
        delegate*<int, int, int> funcPtr = &Add;
        int result = funcPtr(3, 4);

        static int Add(int a, int b) => a + b;
    }
}

// Generic attributes (C# 11)
public class GenericAttribute<T> : Attribute { }

[GenericAttribute<string>]
public class DecoratedClass { }

[GenericAttribute<int>]
public void DecoratedMethod() { }

// UTF-8 string literals (C# 11)
public class Utf8Strings
{
    public ReadOnlySpan<byte> Greeting => "Hello, World!"u8;
    public ReadOnlySpan<byte> Path => "/api/v1/users"u8;
}

// Newlines in string interpolation (C# 11)
public class InterpolationNewlines
{
    public string GetMessage(int score) => $"Result: {score switch
    {
        >= 90 => "Excellent",
        >= 70 => "Good",
        >= 50 => "Pass",
        _ => "Fail"
    }}";
}

// Constant interpolated strings (C# 10)
public class ConstantStrings
{
    const string Scheme = "https";
    const string Host = "example.com";
    const string BaseUrl = $"{Scheme}://{Host}";
}

// Ref struct and scoped (C# 11)
public ref struct RefStructExample
{
    public Span<int> Data;

    public RefStructExample(scoped Span<int> data)
    {
        Data = data;
    }

    public scoped ref int GetFirst() => ref Data[0];
}

// Allows ref struct constraint (C# 13)
public class RefStructConstraint
{
    public static void Process<T>(T value) where T : allows ref struct
    {
        Console.WriteLine(value);
    }
}

// Params collections (C# 13)
public class ParamsCollections
{
    public static void PrintAll(params IEnumerable<string> items)
    {
        foreach (var item in items)
            Console.WriteLine(item);
    }

    public static void PrintSpan(params ReadOnlySpan<int> values)
    {
        foreach (var v in values)
            Console.Write($"{v} ");
    }
}

// Lock object (C# 13)
public class LockExample
{
    private readonly System.Threading.Lock _lock = new();

    public void ThreadSafe()
    {
        lock (_lock)
        {
            Console.WriteLine("Thread-safe operation");
        }
    }
}

public class TypeAliases
{
    public Point Origin => (0, 0);
    public Measurement Distance => (42.5, "meters");
}

// Covariant return types
public class Animal
{
    public virtual Animal Create() => new Animal();
}

public class Cat : Animal
{
    public override Cat Create() => new Cat();
}

// Discard pattern and variable
public class Discards
{
    public void Examples()
    {
        _ = int.TryParse("123", out int result);
        var (_, _, third) = (1, 2, 3);

        switch (42)
        {
            case int _:
                break;
        }

        Action<int, int> ignoreArgs = (_, _) => Console.WriteLine("fired");
    }
}

// Sealed ToString in records
public record SealedRecord(string Name)
{
    public sealed override string ToString() => $"SealedRecord({Name})";
}

// Abstract and virtual static members in interfaces (C# 11)
public interface IParseable<TSelf> where TSelf : IParseable<TSelf>
{
    static abstract TSelf Parse(string s);
    static virtual bool TryParse(string s, out TSelf result)
    {
        try { result = TSelf.Parse(s); return true; }
        catch { result = default!; return false; }
    }
}

// Nameof in attributes (C# 11)
public class NameofInAttributes
{
    [return: MaybeNullWhen(false)]
    public bool TryGetValue([CallerArgumentExpression(nameof(value))] string? expr = null, out int value)
    {
        value = 0;
        return false;
    }
}

// Interpolated string handler (C# 10)
[InterpolatedStringHandler]
public ref struct LogInterpolatedStringHandler
{
    private DefaultInterpolatedStringHandler _inner;

    public LogInterpolatedStringHandler(int literalLength, int formattedCount)
    {
        _inner = new(literalLength, formattedCount);
    }

    public void AppendLiteral(string s) => _inner.AppendLiteral(s);
    public void AppendFormatted<T>(T value) => _inner.AppendFormatted(value);
    public string GetFormattedText() => _inner.ToString();
}

// Nested switch / complex expressions
public class ComplexExpressions
{
    public decimal CalculateDiscount(Customer customer) => customer switch
    {
        { IsVip: true, Orders: > 100 } => 0.20m,
        { IsVip: true } => 0.10m,
        { Orders: > 50 } => 0.05m,
        _ => 0m,
    };

    public string Nested(object[] items) => items switch
    {
        [int x, string s, ..] => $"{x}: {s}",
        [_, _, _, ..] => "3+ items",
        [] => "empty",
        _ => "other"
    };
}

// Preprocessor directives and conditional compilation
#if NET8_0_OR_GREATER
public class Net8Features
{
    public FrozenDictionary<string, int> Cache { get; }
}
#endif

#pragma warning disable CS0168
#pragma warning restore CS0168

#region Modern C# Test Region
public class RegionTest
{
    #region Nested Region
    public void InRegion() { }
    #endregion
}
#endregion


public class AttributeTargets
{
    [field: NonSerialized]
    public int AutoProp { get; set; }

    [method: Obsolete("Deprecated")]
    public event EventHandler? MyEvent;

    [property: JsonPropertyName("value")]
    public int Value { get; set; }
}

// Span and stackalloc
public class SpanExamples
{
    public void Method()
    {
        Span<int> numbers = stackalloc int[10];
        ReadOnlySpan<char> text = "hello".AsSpan();
        Span<byte> buffer = stackalloc byte[] { 0x00, 0xFF, 0xAB };

        bool found = numbers[..5] is [0, 0, 0, 0, 0];
    }
}

// Struct improvements
public struct MutableStruct
{
    public int X;
    public readonly int Y;
    public readonly void ReadOnlyMethod() => Console.WriteLine(Y);
    public readonly override string ToString() => $"({X}, {Y})";
}

// String and char literals
public class LiteralExamples
{
    public string Verbatim = @"C:\Users\test\file.txt";
    public string Escaped = "Line1\nLine2\tTabbed";
    public string Unicode = "\u0041\u0042\u0043";
    public char SingleChar = 'A';
    public char EscapedChar = '\n';
    public string RawQuotes = """He said "hello" and left.""";
    public int Binary = 0b1010_1100;
    public int Hex = 0xFF_AA;
    public long BigNumber = 1_000_000_000L;
    public double Scientific = 1.5e10;
    public decimal Money = 99.99m;
    public float Ratio = 0.5f;
}

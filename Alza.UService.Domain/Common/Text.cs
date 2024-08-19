namespace Alza.UService.Domain.Common;

/// <summary>
/// Non-empty short text.
/// The allowed maximum length is 32 characters that is succifient for the demo project.
/// </summary>
public class Text : ValueObject
{
    private const int MaxLength = 32;

    /// <summary>
    /// Creates a new short text object.
    /// The input value is always trimmed.
    /// The value cannot be empty or contain only whitespaces.
    /// </summary>
    public static Result<Text> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Failure<Text>($"The value is required");
        }

        var trimmedValue = value.Trim();

        if (trimmedValue.Length > MaxLength)
        {
            return Result.Failure<Text>($"The value is too long. The allowed maximum length is {MaxLength} characters");
        }

        return new Text(trimmedValue);
    }

    private Text(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static explicit operator string(Text text) => text.Value;

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return Value;
    }
}

using System.Data;
using DictionaryService.Domain.Common;

namespace DictionaryService.Domain.ValueObjects;

public record class NonEmptyString
{
    public string Value { get;}

    private NonEmptyString(string value)
    {
        Value = value;
    }

    public static NonEmptyString Create(string value, string paramName = "Value")
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException($"{paramName} cannot be empty or whitespace.");
        }

        if (value.Length > 255)
        {
            throw new DataException($"{paramName} cannot exceed 255 characters.");
        }
        
        return new NonEmptyString(value.Trim());
    }
}
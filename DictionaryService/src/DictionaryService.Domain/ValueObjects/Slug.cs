using System.Data;
using DictionaryService.Domain.Common;

namespace DictionaryService.Domain.ValueObjects;

public class Slug
{
    public string Value { get;}

    private Slug(string value)
    {
        Value = value;
    }

    public static Slug Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("Slug cannot be empty.");
        }

        string normalized = value.Trim().ToLowerInvariant();

        if (!System.Text.RegularExpressions.Regex.IsMatch(normalized, @"^[a-z0-9_-]+$"))
        {
            throw new DomainException("Slug can only contain lowercase letters, numbers, hyphens and underscores.");
        }

        if (normalized.Length > 100)
        {
            throw new DomainException("Slug cannot exceed 100 characters.");
        }
        
        return new Slug(normalized);
    }
}

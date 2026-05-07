using DictionaryService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using HierarchyPathValue = DictionaryService.Domain.ValueObjects.HierarchyPath;
using NonEmptyStringValue = DictionaryService.Domain.ValueObjects.NonEmptyString;
using SlugValue = DictionaryService.Domain.ValueObjects.Slug;

namespace DictionaryService.Infrastructure.Postgres.Configurations;

internal static class ValueObjectConverters
{
    public static readonly ValueConverter<NonEmptyStringValue, string> NonEmptyString =
        new(value => value.Value, value => NonEmptyStringValue.Create(value, "Value"));

    public static readonly ValueConverter<NonEmptyStringValue?, string?> NullableNonEmptyString =
        new(value => value == null ? null : value.Value, value => value == null ? null : NonEmptyStringValue.Create(value, "Value"));

    public static readonly ValueConverter<SlugValue, string> Slug =
        new(value => value.Value, value => SlugValue.Create(value));

    public static readonly ValueConverter<HierarchyPathValue, string> HierarchyPath =
        new(value => value.Value, value => HierarchyPathValue.Restore(value));
}

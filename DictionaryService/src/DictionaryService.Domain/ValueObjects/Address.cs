using DictionaryService.Domain.Common;

namespace DictionaryService.Domain.ValueObjects;

public class Address
{
    public string Country { get; private set; }
    public string City { get; private set; }
    public string Street { get; private set; }
    public string? Building { get; private set; }
    public string? OfficeNumber { get; private set; }

    private Address()
    {
        Country = null!;
        City = null!;
        Street = null!;
    }

    public Address(string country, string city, string street, string? building, string? officeNumber)
    {
        Country = country;
        City = city;
        Street = street;
        Building = building;
        OfficeNumber = officeNumber;
    }

    public static Address Create(string country, string city, string street, string? building, string? officeNumber)
    {
        if (string.IsNullOrWhiteSpace(country))
            throw new DomainException("Country cannot be empty.");
        
        if (string.IsNullOrWhiteSpace(city))
            throw new DomainException("City cannot be empty.");
        
        if (string.IsNullOrWhiteSpace(street))
            throw new DomainException("Street cannot be empty.");
        
        if (country.Length > 100)
            throw new DomainException("Country name too long.");
        
        if (city.Length > 100)
            throw new DomainException("City name too long.");
        
        if (street.Length > 200)
            throw new DomainException("Street name too long.");
        
        return new Address(country.Trim(), city.Trim(), street.Trim(), building?.Trim(), officeNumber?.Trim());
    }
}

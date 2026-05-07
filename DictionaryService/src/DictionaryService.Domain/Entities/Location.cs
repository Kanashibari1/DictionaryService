using DictionaryService.Domain.ValueObjects;

namespace DictionaryService.Domain.Entities;

public class Location
{
    private readonly List<DepartmentPosition> _departments = new();
    public IReadOnlyList<DepartmentPosition> Departments => _departments;

    public Guid Id { get; private set; }
    public NonEmptyString Name { get; private set; }
    public Address Address { get; private set; }

    private Location()
    {
        Name = null!;
        Address = null!;
    }

    private Location(Guid id, NonEmptyString name, Address address)
    {
        Id = id;
        Name = name;
        Address = address;
    }

    public static Location Create(NonEmptyString name, Address address)
    {
        return new Location(Guid.NewGuid(), name, address);
    }

    public void Rename(NonEmptyString newName)
    {
        Name = newName;
    }
    
    public void Relocate(Address newAddress)
    {
        Address = newAddress;
    }

    public void AddDepartment(DepartmentPosition department)
    {
        if (!_departments.Contains(department))
        {
            _departments.Add(department);
        }
    }

    public void RemoveDepartment(DepartmentPosition department)
    {
        _departments.Remove(department);
    }
}

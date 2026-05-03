using DictionaryService.Domain.ValueObjects;

namespace DictionaryService.Domain.Entities;

public class Location
{
    private readonly List<Department> _departments = new();
    public IReadOnlyList<Department> Departments => _departments;

    private Location(Guid id, NonEmptyString name, Address address)
    {
        Id = id;
        Name = name;
        Address = address;
    }
    
    public Guid Id { get; }
    public NonEmptyString Name { get; private set; }
    public Address Address { get; private set; }

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

    public void AddDepartment(Department department)
    {
        if (!_departments.Contains(department))
        {
            _departments.Add(department);
        }
    }

    public void RemoveDepartment(Department department)
    {
        _departments.Remove(department);
    }
}
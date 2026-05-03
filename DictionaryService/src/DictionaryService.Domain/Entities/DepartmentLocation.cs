using DictionaryService.Domain.Common;

namespace DictionaryService.Domain.Entities;

public class DepartmentLocation
{
    public Guid DepartmentId { get; private set; }
    public Department Department { get; private set; }
    
    public Guid LocationId { get; private set; }
    public Location Location { get; private set; }

    public DepartmentLocation(Department department, Location location)
    {
        Department = department;
        Location = location;
        DepartmentId = department.Id;
        LocationId = location.Id;
    }

    public static DepartmentLocation Create(Department department, Location location)
    {
        if (department == null)
        {
            throw new DomainException("Department cannot be null");
        }

        if (location == null)
        {
            throw new DomainException("Location cannot be null");
        }
        
        var departmentLocation = new DepartmentLocation(department, location);
        
        return departmentLocation;
    }
}
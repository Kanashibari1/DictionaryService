using DictionaryService.Domain.Common;

namespace DictionaryService.Domain.Entities;

public class DepartmentPosition
{
    public Guid DepartmentId { get;private set; }
    public Department Department { get; private set; }
    
    public Guid PostId { get; private set; }
    public Post Post { get; private set; }

    private DepartmentPosition(Department department, Post post)
    {
        Department = department ?? throw new DomainException("Department cannot be null");
        Post = post?? throw new DomainException("Position cannot be null");
    }

    public static DepartmentPosition Create(Department department, Post post)
    {
        if (department == null) 
            throw new DomainException("Department cannot be null");
        
        if (post == null)
            throw new DomainException("Post cannot be null");
        
        var departmentPosition  = new DepartmentPosition(department, post);
        
        return departmentPosition;
    }
}
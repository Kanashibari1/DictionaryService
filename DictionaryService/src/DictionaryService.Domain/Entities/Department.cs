using DictionaryService.Domain.Common;
using DictionaryService.Domain.ValueObjects;

namespace DictionaryService.Domain.Entities;

public class Department
{
    public Guid Id { get; }
    public NonEmptyString Name { get; private set; }
    public Slug Slug { get; private set; }
    public HierarchyPath HierarchyPath { get; private set; }
    public Guid? ParentId { get; private set; }
    public Department? Parent { get; private set; }
    
    private readonly List<Department> _children = new();
    public IReadOnlyList<Department> Children => _children;
    
    private readonly List<DepartmentLocation> _locations = new();
    public IReadOnlyList<DepartmentLocation> Locations => _locations;
    
    private readonly List<DepartmentPosition> _post = new();
    public IReadOnlyList<DepartmentPosition> Post => _post;

    private Department(Guid id, NonEmptyString name, Slug slug, HierarchyPath hierarchyPath, Guid? parentId = null)
    {
        Id = id;
        Name = name;
        Slug = slug;
        HierarchyPath = hierarchyPath;
        ParentId = parentId;
    }

    public static Department CreateRoot(NonEmptyString name, Slug slug)
    {
        Guid id = Guid.NewGuid();
        var path = HierarchyPath.CreateRoot(id);
        return new Department(id, name, slug, path);
    }

    public static Department CreateChild(NonEmptyString name, Slug slug, Department parent)
    {
        if (parent == null)
        {
            throw new DomainException("Parent department cannot be null.");
        }
        
        Guid id = Guid.NewGuid();
        var path = HierarchyPath.CreateChild(parent.HierarchyPath, id);
        var child = new Department(id, name, slug, path, parent.Id);
        
        child.SetParent(parent);
        
        return child;
    }

    public void SetParent(Department department)
    {
        Parent = department;
        department._children.Add(this);
    }

    public void Rename(NonEmptyString newName)
    {
        Name = newName;
    }
    
    public void ChangeSlug(Slug newSlug)
    {
        Slug = newSlug;
    }

    public DepartmentLocation AddLocation(Location location)
    {
        if (location == null)
        {
            throw new DomainException("Location cannot be null.");
        }
        
        return DepartmentLocation.Create(this, location);
    }

    public void RemoveLocation(Location location)
    {
        if (location == null)
        {
            throw new DomainException("Location cannot be null.");
        }
        
        var departmentLocation = _locations.FirstOrDefault(dl => dl.LocationId == location.Id);

        if (departmentLocation == null)
        {
            throw new DomainException($"Department is not linked to location '{location.Name}'");
        }
        
        _locations.Remove(departmentLocation);
    }

    public DepartmentPosition AddPost(Post post)
    {
        if (post == null)
        {
            throw new DomainException("Post cannot be null.");
        }

        return DepartmentPosition.Create(this, post);
    }

    public void RemovePost(Post post)
    {
        if (post == null)
        {
            throw new DomainException("Position cannot be null.");
        }
        
        DepartmentPosition? departmentPost = _post.FirstOrDefault(dp => dp.PostId == post.Id);

        if (departmentPost != null)
        {
            _post.Remove(departmentPost);
        }
    }
}
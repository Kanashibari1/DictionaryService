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
    
    private readonly List<Location> _locations = new();
    public IReadOnlyList<Location> Locations => _locations;
    
    private readonly List<Post> _post = new();
    public IReadOnlyList<Post> Post => _post;

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

    public void AddLocation(Location location)
    {
        if (location == null)
        {
            throw new DomainException("Location cannot be null.");
        }
        
        if(!_locations.Contains(location))
        {
            _locations.Add(location);
            location.AddDepartment(this);
        }
    }

    public void RemoveLocation(Location location)
    {
        if (_locations.Remove(location))
        {
            location.RemoveDepartment(this);
        }
    }

    public void AddPosition(Post post)
    {
        if (post == null)
        {
            throw new DomainException("Post cannot be null.");
        }

        if (!_post.Contains(post))
        {
            _post.Add(post);
            post.AddDepartment(this);
        }
    }

    public void RemovePost(Post post)
    {
        if (_post.Remove(post))
        {
            post.RemoveDepartment(this);
        }
    }
}
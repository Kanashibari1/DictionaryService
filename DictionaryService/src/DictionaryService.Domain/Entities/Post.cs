using DictionaryService.Domain.ValueObjects;

namespace DictionaryService.Domain.Entities;

public class Post
{
    public Guid Id { get;}
    public NonEmptyString Title { get; private set; }
    public NonEmptyString Description { get; private set; }
    
    private readonly List<Department> _departments;
    public List<Department> Departments => _departments;

    private Post(Guid id, NonEmptyString title, NonEmptyString description = null)
    {
        Id = id;
        Title = title;
        Description = description;
    }

    public static Post Create(NonEmptyString title, NonEmptyString? description = null)
    {
        return new Post(Guid.NewGuid(), title, description);
    }

    public void ChangeTitle(NonEmptyString newTitle)
    {
        Title = newTitle;
    }

    public void UpdateDescription(NonEmptyString newDescription)
    {
        Description = newDescription;
    }

    public void AddDepartment(Department department)
    {
        if (!_departments.Contains(department))
            _departments.Add(department);
    }

    public void RemoveDepartment(Department department)
    {
        _departments.Remove(department);
    }
}
using DictionaryService.Domain.Common;

namespace DictionaryService.Domain.ValueObjects;

public class HierarchyPath
{
    private static readonly char Separator = '/';
    
    public string Value { get; }
    
    public IReadOnlyList<Guid> Ancestors { get; }

    private HierarchyPath(string value, IReadOnlyList<Guid> ancestors)
    {
        Value = value;
        Ancestors = ancestors;
    }

    public static HierarchyPath CreateRoot(Guid departmentId)
    {
        string path = $"/{departmentId}/";
        
        return new HierarchyPath(path, new List<Guid> {departmentId});
    }

    public static HierarchyPath CreateChild(HierarchyPath parentPath, Guid childId)
    {
        if (parentPath == null)
        {
            throw new DomainException("Parent path cannot be null.");
        }

        string newPath = $"{parentPath.Value}/{childId}/";
        List<Guid> newAncestors = parentPath.Ancestors.Append(childId).ToList();
        
        return new HierarchyPath(newPath, newAncestors);
    }
}
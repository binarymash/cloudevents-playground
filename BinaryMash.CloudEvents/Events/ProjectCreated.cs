namespace BinaryMash.CloudEvents.Events;

public class ProjectCreated
{
    public Guid Id { get; set; } = default;
    
    
    public string Name { get; set; } = default!;
}
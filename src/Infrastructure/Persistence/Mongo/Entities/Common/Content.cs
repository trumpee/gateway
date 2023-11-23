namespace Infrastructure.Persistence.Mongo.Entities.Common;

public class Content
{
    public string? Subject { get; set; }
    public string? Body { get; set; }

    public Dictionary<string, VariableDescriptor>? Variables { get; set; }
}
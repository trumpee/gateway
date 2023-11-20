namespace Infrastructure.Persistence.Mongo.Entities;

public class TemplateContent
{
    public string? Subject { get; set; }
    public string? Body { get; set; }

    public Dictionary<string, VariableDescriptor>? Variables { get; set; }
}
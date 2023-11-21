namespace Infrastructure.Persistence.Mongo.Entities.Template;

public class TemplateContent
{
    public string? Subject { get; set; }
    public string? Body { get; set; }

    public Dictionary<string, VariableDescriptor>? Variables { get; set; }
}
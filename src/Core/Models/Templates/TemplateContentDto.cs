namespace Core.Models.Templates;

public class TemplateContentDto
{
    public string? Subject { get; init; }
    public string? Body { get; init; }

    public Dictionary<string, VariableDescriptorDto>? Variables { get; set; }
}
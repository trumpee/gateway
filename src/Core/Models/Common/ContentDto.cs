namespace Core.Models.Common;

public class ContentDto
{
    public string? Subject { get; init; }
    public string? Body { get; init; }

    public Dictionary<string, VariableDescriptorDto>? Variables { get; set; }
}
using System.Globalization;
using System.Text;

namespace Core.Extensions;

public static class StringExtensions
{
    public static string ToPascalCase(this string text)
    {
        if (text.Length < 1)
            return text;
        
        var words = text.Split(new[] { ' ', '-', '_', '.' }, StringSplitOptions.RemoveEmptyEntries);
        
        var sb = new StringBuilder();
        
        foreach (var word in words)
        {
            if (word.Length > 0)
            {
                sb.Append(CultureInfo.InvariantCulture.TextInfo.ToTitleCase(word.ToLowerInvariant()));
            }
        }
        
        return sb.ToString();
    }
}
using System.Globalization;
using System.Text;

namespace Auth.Api.Extensions;

public static class StringExtensions
{
    public static string ConvertToAsciiLowercase(this string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        input = input.Replace("Đ", "D").Replace("đ", "d");

        // Normalize string to decompose diacritics
        string normalizedString = input.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder();

        foreach (char c in normalizedString)
        {
            UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }

        // Convert to ASCII and remove spaces
        string asciiString = stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        return new string([.. asciiString
            .ToLower()
            .Where(c => !char.IsWhiteSpace(c))]);
    }
}

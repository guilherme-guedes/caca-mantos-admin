using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace backend.Utils
{
    public class StringUtils
    {        
        public static string RemoverAcentos(string trecho)
        {
            var normalizedString = trecho.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder(capacity: normalizedString.Length);

            for (int i = 0; i < normalizedString.Length; i++)
            {
                char c = normalizedString[i];
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
        
        public static string SanitizarBusca(string trecho) =>
            Regex.Replace(RemoverAcentos(trecho), @"(\s){2,}", " ");
    }
}
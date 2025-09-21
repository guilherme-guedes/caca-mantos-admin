using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace CacaMantos.Admin.API.Common.Utils
{
    public static class StringUtils
    {
        public static string RemoverAcentos(string trecho)
        {
            if (string.IsNullOrEmpty(trecho))
                return string.Empty;

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

        public static string SanitizarTrechoBusca(string trecho) =>
            ReduzirEspacos(RemoverAcentos(RemoverEspacosInicioFim(trecho)));

        public static string ReduzirEspacos(string termo)
        {
            if (string.IsNullOrEmpty(termo)) return string.Empty;
            return Regex.Replace(termo, @"(\s){2,}", " ");
        }

        public static string RemoverEspacosInicioFim(string termo)
        {
            if (string.IsNullOrEmpty(termo)) return string.Empty;
            return Regex.Replace(termo, @"^\s+|\s+$", "");
        }
    }
}

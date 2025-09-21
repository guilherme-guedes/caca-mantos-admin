namespace CacaMantos.Admin.API.Common.Utils
{
    public static class UrlUtils
    {
        public static bool UrlValida(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out Uri validatedUri)
                   && (validatedUri.Scheme == Uri.UriSchemeHttp || validatedUri.Scheme == Uri.UriSchemeHttps);
        }
    }
}

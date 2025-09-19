namespace CacaMantos.Admin.API.Application.DTO
{
    public record CriacaoLojaRequest(
        string Nome,
        string Site,
        string UrlBusca,
        bool Parceira,
        bool Ativa,
        IList<string> Times);
}
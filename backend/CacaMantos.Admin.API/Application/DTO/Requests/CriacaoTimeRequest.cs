namespace CacaMantos.Admin.API.Application.DTO
{
    public record CriacaoTimeRequest(
        string Nome,
        string Identificador,
        string NomeBusca,
        IList<string> Termos,
        bool Destaque,
        bool Ativo,
        bool Principal,
        IList<string> Homonimos,
        string TimePrincipal);
}
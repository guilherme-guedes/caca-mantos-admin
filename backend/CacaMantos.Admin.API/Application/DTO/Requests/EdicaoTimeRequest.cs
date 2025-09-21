using Microsoft.AspNetCore.Mvc;

namespace CacaMantos.Admin.API.Application.DTO
{
    public record EdicaoTimeRequest(
        [FromRoute(Name = "id")] string Id,
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

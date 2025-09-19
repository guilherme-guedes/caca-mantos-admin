using Microsoft.AspNetCore.Mvc;

namespace CacaMantos.Admin.API.Application.DTO
{
    public record EdicaoLojaRequest(
        [FromRoute(Name = "id")] String Id,
        String Nome,
        String Site,
        String UrlBusca,
        bool Parceira,
        bool Ativa,
        IList<String> Times);   
}
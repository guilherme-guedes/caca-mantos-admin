namespace CacaMantos.Admin.API.Application.DTO
{
    public record LojaResponse(
        string Id,
        string Nome,
        string Site,
        string UrlBusca,
        bool Parceira,
        bool Ativa,
        IList<TimeResumidoDTO> Times);
}
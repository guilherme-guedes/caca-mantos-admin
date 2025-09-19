namespace CacaMantos.Admin.API.Application.DTO.Responses
{
    public record TimeResponse(
        string Id,
        string Nome,
        string Identificador,
        string NomeBusca,
        IList<string> Termos,
        bool Destaque,
        bool Ativo,
        bool Principal,
        IList<TimeResumidoDTO> Homonimos,
        TimeResumidoDTO TimePrincipal);    
}
using Microsoft.AspNetCore.Mvc;

namespace backend.Application.DTO
{
    public record PesquisaPaginadaTimeRequest(
        [FromQuery(Name="pagina")] int? Pagina,
        [FromQuery(Name="quantidade")] int? TamanhoPagina,
        [FromQuery(Name="trecho")] string Trecho,
        [FromQuery(Name="destaque")] bool? Destaque,
        [FromQuery(Name="ativo")] bool? Ativo,
        [FromQuery(Name="principal")] bool? Principal
    ) : PesquisaPaginadaRequest(Pagina, TamanhoPagina);
}
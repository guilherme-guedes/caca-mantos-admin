using Microsoft.AspNetCore.Mvc;

namespace backend.Application.DTO
{
    public record PesquisaPaginadaLojaRequest(
        [FromQuery(Name = "pagina")] int? Pagina,
        [FromQuery(Name = "quantidade")] int? TamanhoPagina,
        [FromQuery(Name = "trecho")] string Trecho,
        [FromQuery(Name = "parceira")] bool? Parceira,
        [FromQuery(Name = "ativa")] bool? Ativa
    ) : PesquisaPaginadaRequest(Pagina, TamanhoPagina);
}

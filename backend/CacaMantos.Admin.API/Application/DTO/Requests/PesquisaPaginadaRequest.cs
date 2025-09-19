using Microsoft.AspNetCore.Mvc;

namespace backend.Application.DTO
{
    public record PesquisaPaginadaRequest(        
        [FromQuery(Name="pagina")] int? Pagina,
        [FromQuery(Name="quantidade")] int? TamanhoPagina
    );
}
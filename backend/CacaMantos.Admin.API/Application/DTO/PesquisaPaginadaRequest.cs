using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Application.DTO
{
    public class PesquisaPaginadaRequest
    {        
        [FromQuery(Name="pagina")]
        public int? Pagina { get; set; }

        [FromQuery(Name="quantidade")]
        public int? TamanhoPagina { get; set; }
    }
}
using System.Text.Json.Serialization;

namespace backend.Application.DTO
{
    public class PesquisaPaginadaRequest
    {        
        [JsonPropertyName("pagina")]
        public int? Pagina { get; set; }

        [JsonPropertyName("quantidade")]
        public int? TamanhoPagina { get; set; }
    }
}
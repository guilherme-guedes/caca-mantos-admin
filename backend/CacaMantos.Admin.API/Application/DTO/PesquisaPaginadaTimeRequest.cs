using System.Text.Json.Serialization;

namespace backend.Application.DTO
{
    public class PesquisaPaginadaTimeRequest : PesquisaPaginadaRequest
    {
        [JsonPropertyName("trecho")]
        public string Trecho { get; set; }

        [JsonPropertyName("destaque")]
        public bool? Destaque { get; set; }

        [JsonPropertyName("ativo")]
        public bool? Ativo { get; set; }

        [JsonPropertyName("principal")]
        public bool? Principal { get; set; }  
    }
}
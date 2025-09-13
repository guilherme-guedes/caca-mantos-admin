using System.Text.Json.Serialization;

namespace backend.Application.DTO
{
    public class PesquisaPaginadaLojaRequest : PesquisaPaginadaRequest
    {   
        [JsonPropertyName("trecho")]
        public string Trecho { get; set; }

        [JsonPropertyName("parceira")]
        public bool? Parceira  { get; set; }

        [JsonPropertyName("ativo")]
        public bool? Ativo  { get; set; }
    }
}
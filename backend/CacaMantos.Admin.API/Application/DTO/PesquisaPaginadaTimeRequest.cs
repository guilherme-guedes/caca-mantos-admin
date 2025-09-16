using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Application.DTO
{
    public class PesquisaPaginadaTimeRequest : PesquisaPaginadaRequest
    {
        [FromQuery(Name="trecho")]
        public string Trecho { get; set; }

        [FromQuery(Name="destaque")]
        public bool? Destaque { get; set; }

        [FromQuery(Name="ativo")]
        public bool? Ativo { get; set; }

        [FromQuery(Name="principal")]
        public bool? Principal { get; set; }  
    }
}
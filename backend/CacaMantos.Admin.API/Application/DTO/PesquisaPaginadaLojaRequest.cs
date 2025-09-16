using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Application.DTO
{
    public class PesquisaPaginadaLojaRequest : PesquisaPaginadaRequest
    {   
        [FromQuery(Name="trecho")]
        public string Trecho { get; set; }

        [FromQuery(Name="parceira")]
        public bool? Parceira  { get; set; }

        [FromQuery(Name="ativa")]
        public bool? Ativa  { get; set; }
    }
}
using Microsoft.AspNetCore.Mvc;

namespace CacaMantos.Admin.API.Application.DTO
{
    public class EdicaoLojaRequest
    {
        [FromRoute(Name = "id")]
        public String Id { get; set; }
        
        public String Nome { get; set; }
        public String Site { get; set; }
        public String UrlBusca { get; set; }
        public bool Parceira { get; set; }
        public bool Ativa { get; set; }
        public IList<String> Times { get; set; }
    }
}
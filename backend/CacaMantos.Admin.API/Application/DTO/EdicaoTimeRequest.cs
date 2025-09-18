using Microsoft.AspNetCore.Mvc;

namespace CacaMantos.Admin.API.Application.DTO
{
    public class EdicaoTimeRequest
    {
        [FromRoute(Name = "id")]
        public String Id { get; set; }
        
        public String Nome { get; private set; }
        public String Identificador { get; private set; }
        public String NomeBusca { get; private set; }
        public IList<String> Termos { get; private set; }
        public bool Destaque { get; private set; }
        public bool Ativo { get; private set; }
        public bool Principal { get; private set; }
        public IList<String> Homonimos { get; private set; }
        public String TimePrincipal { get; private set; }
    }
}
namespace CacaMantos.Admin.API.Application.DTO
{
    public class CriacaoLojaRequest
    {
        public String Nome { get; set; }
        public String Site { get; set; }
        public String UrlBusca { get; set; }
        public bool Parceira { get; set; }
        public bool Ativa { get; set; }
        public IList<string> Times { get; set; }        
    }
}
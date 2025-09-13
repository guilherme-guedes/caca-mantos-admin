namespace backend.Domain.Pesquisas
{
    public class PesquisaPaginadaTime : PesquisaPaginada
    {
        public string Trecho { get; set; }
        public bool? Destaque { get; set; }
        public bool? Ativo { get; set; }
        public bool? Principal { get; set; }

        public PesquisaPaginadaTime(int pagina = 1, int tamanhoPagina = 5, string trecho = null, bool? destaque = null, bool? ativo = null, bool? principal = null)
            : base(pagina, tamanhoPagina)
        {
            Trecho = trecho;
            Destaque = destaque;
            Ativo = ativo;
            Principal = principal;
        }

        public bool TemTrechoInformado() => !string.IsNullOrEmpty(Trecho);
        public bool TemDestaqueInformado() => Destaque.HasValue;
        public bool TemAtivoInformado() => Ativo.HasValue;
        public bool TemPrincipalInformado() => Principal.HasValue;  
    }
}
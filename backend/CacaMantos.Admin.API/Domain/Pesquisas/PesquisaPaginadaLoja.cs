namespace backend.Domain.Pesquisas
{
    public class PesquisaPaginadaLoja : PesquisaPaginada
    {
        public string Trecho  { get; set; }
        public bool? Parceira  { get; set; }
        public bool? Ativo  { get; set; }

        public PesquisaPaginadaLoja(int pagina = 1, int tamanhoPagina = 5, string trecho = null, bool? parceira = null, bool? ativo = null)
            : base(pagina, tamanhoPagina)
        {
            this.Trecho = trecho;
            this.Parceira = parceira;
            this.Ativo = ativo;
        }

        public bool TemTrechoInformado() => !string.IsNullOrEmpty(Trecho);
        public bool TemParceiraInformado() => Parceira.HasValue;
        public bool TemAtivoInformado() => Ativo.HasValue;
    }
}
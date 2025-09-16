namespace backend.Domain.Pesquisas
{
    public class PesquisaPaginada
    {
        public int Pagina { get; set; } = 1;
        public int TamanhoPagina { get; set; } = 5;   
           
        public PesquisaPaginada(int pagina = 1, int tamanhoPagina = 5)
        {
            Pagina = pagina < 1 ? 1 : pagina;
            TamanhoPagina = tamanhoPagina < 1 ? 5 : tamanhoPagina;
        }  
    }
}
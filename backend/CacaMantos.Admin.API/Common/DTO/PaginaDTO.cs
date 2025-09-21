namespace CacaMantos.Admin.API.Common.DTO
{
    public record PaginaDTO<T>
    {
        public int PaginaAtual { get; set; }
        public int TotalPaginas { get; set; }
        public int ItensPorPagina { get; set; }
        public int QuantidadeTotal { get; set; }
        public IList<T> Itens { get; private set; }

        public PaginaDTO(int paginaAtual, int itensPorPagina, int total, IList<T> itens)
        {
            PaginaAtual = paginaAtual;
            TotalPaginas = (int) Math.Ceiling((decimal) total / itensPorPagina);
            ItensPorPagina = itensPorPagina;
            QuantidadeTotal = total;
            Itens = itens;
        }

        public static PaginaDTO<T> Vazia(int paginaAtual, int itensPorPagina)
        {
            return new PaginaDTO<T>(paginaAtual, itensPorPagina, 0, new List<T>());
        }
    }
}

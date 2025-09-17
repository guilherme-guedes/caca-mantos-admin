namespace backend.Domain.Entities
{
    public class Loja
    {
        public Guid Id { get; private set; }
        public String Nome { get; private set; }
        public String Site { get; private set; }
        public String UrlBusca { get; private set; }
        public bool Parceira { get; private set; }
        public bool Ativa { get; private set; }
        public IList<Time> Times { get; private set; }

        public Loja(Guid id,
                    String nome,
                    String site,
                    String urlBusca,
                    bool parceira,
                    bool ativa = true,
                    IList<Time> times = null)
        {
            this.Id = id;
            this.Nome = nome;
            this.Site = site;
            this.UrlBusca = urlBusca;
            this.Parceira = parceira;
            this.Ativa = ativa;
            this.Times = times ?? new List<Time>();
        }        
    }
}
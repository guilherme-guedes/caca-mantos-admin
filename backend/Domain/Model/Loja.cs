namespace backend.Domain.Model
{
    public class Loja
    {
        public String id { get; private set; }
        public String nome { get; private set; }
        public String site { get; private set; }
        public String urlBusca { get; private set; }
        public bool parceira { get; private set; }
        public bool ativa { get; private set; }
        public IList<Time> times { get; private set; }

        public Loja(String id)
        {
            this.id = id;
        }

        public Loja(String id,
                    String nome,
                    String site,
                    String urlBusca,
                    bool parceira,
                    bool ativa = true,
                    IList<Time> times = null)
        {
            this.id = id;
            this.nome = nome;
            this.site = site;
            this.urlBusca = urlBusca;
            this.parceira = parceira;
            this.ativa = ativa;
            this.times = times;
        }
        
    }
}
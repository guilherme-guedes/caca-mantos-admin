namespace backend.Domain.Entities
{
    public class Time
    {
        public Guid id { get; private set; }
        public String nome { get; private set; }
        public String identificador { get; private set; }
        public String nomeBusca { get; private set; }
        public IList<String> termos { get; private set; }
        public bool destaque { get; private set; }
        public bool ativo { get; private set; }
        public bool principal { get; private set; }
        public IList<Time> homonimos { get; private set; }

        public Time(Guid id,
                    String nome,
                    String identificador,
                    String nomeBusca,
                    IList<String> termos = null,
                    bool destaque = false,
                    bool ativo = true,
                    bool principal = true,
                    IList<Time> homonimos = null)
        {
            this.id = id;
            this.nome = nome;
            this.identificador = identificador;
            this.nomeBusca = nomeBusca;
            this.termos = termos;
            this.destaque = destaque;
            this.ativo = ativo;
            this.principal = principal;
            this.homonimos = homonimos;
        }

        public void AdicionarHomonimo(Time homonimo)
        {
            if (!this.principal)
                throw new InvalidOperationException("Não é possível adicionar times homônimos a um time que não é principal.");

            if (homonimo == null)
                throw new ArgumentNullException(nameof(homonimo));

            if (homonimos == null)
                homonimos = new List<Time>();

            homonimos.Add(homonimo);
        }

        public void AdicionarTermo(String termo)
        {
            if (String.IsNullOrEmpty(termo))
                return;

            if (termos == null)
                termos = new List<String>();

            termos.Add(termo);
        }
        
        public bool TemTimesHomonimos() =>  homonimos?.Count > 0;  
              
        public bool TemTermos() => termos?.Count > 0;        
    }
}
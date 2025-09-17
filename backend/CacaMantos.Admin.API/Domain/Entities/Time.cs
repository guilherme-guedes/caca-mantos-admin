namespace backend.Domain.Entities
{
    public class Time
    {
        public Guid Id { get; private set; }
        public String Nome { get; private set; }
        public String Identificador { get; private set; }
        public String NomeBusca { get; private set; }
        public IList<String> Termos { get; private set; }
        public bool Destaque { get; private set; }
        public bool Ativo { get; private set; }
        public bool Principal { get; private set; }
        public IList<Time> Homonimos { get; private set; }
        public Time TimePrincipal { get; private set; }

        public Time(Guid id,
                    String nome,
                    String identificador,
                    String nomeBusca,
                    IList<String> termos = null,
                    bool destaque = false,
                    bool ativo = true,
                    bool principal = true,
                    IList<Time> homonimos = null,
                    Time timePrincipal = null)
        {
            this.Id = id;
            this.Nome = nome;
            this.Identificador = identificador;
            this.NomeBusca = nomeBusca;
            this.Termos = termos;
            this.Destaque = destaque;
            this.Ativo = ativo;
            this.Principal = principal;
            this.Homonimos = homonimos;
            this.TimePrincipal = timePrincipal;
        }

        public void AdicionarHomonimo(Time homonimo)
        {
            if (!this.Principal)
                throw new InvalidOperationException("Não é possível adicionar times homônimos a um time que não é principal.");

            if (homonimo == null)
                throw new ArgumentNullException(nameof(homonimo));

            if (Homonimos == null)
                Homonimos = new List<Time>();

            Homonimos.Add(homonimo);
        }

        public void AdicionarTermo(String termo)
        {
            if (String.IsNullOrEmpty(termo))
                return;

            if (Termos == null)
                Termos = new List<String>();

            Termos.Add(termo);
        }
        
        public bool TemTimesHomonimos() =>  Homonimos?.Count > 0;  
              
        public bool TemTimePrincipal() => TimePrincipal != null;
        
        public bool TemTermos() => Termos?.Count > 0;        
    }
}
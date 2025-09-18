using CacaMantos.Admin.API.Domain.Entities;

namespace backend.Domain.Entities
{
    public class Time :  EntidadeBase
    {
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
            if(principal && timePrincipal != null)
                throw new InvalidOperationException("Não é possível definir um time principal para um time que é principal.");
            if(!principal && homonimos != null && homonimos.Count > 0)
                throw new InvalidOperationException("Não é possível adicionar times homônimos a um time que não é principal.");
                
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

        public void AlterarTimePrincipal(Time timePrincipal)
        {
            if (this.Principal)
                throw new InvalidOperationException("Não é possível definir um time principal para um time que é principal.");

            this.TimePrincipal = timePrincipal;
            this.Homonimos.Clear();
        }

        public void AlterarTimesHomonimos(IList<Time> homonimos)
        {
            if( homonimos == null || homonimos.Count == 0)
            {
                this.Homonimos = new List<Time>();
                return;
            }

            if (!this.Principal)
                throw new InvalidOperationException("Não é possível adicionar times homônimos a um time que não é principal.");

            this.Homonimos = homonimos;
            this.TimePrincipal = null;
        }

        public void AdicionarHomonimo(Time homonimo)
        {
            if (!this.Principal)
                throw new InvalidOperationException("Não é possível adicionar times homônimos a um time que não é principal.");

            if (homonimo == null)
                throw new ArgumentNullException("Time homônimo não informado", nameof(homonimo));

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
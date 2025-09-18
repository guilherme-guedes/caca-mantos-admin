using CacaMantos.Admin.API.Common.Utils;
using CacaMantos.Admin.API.Domain.Entities;
using CacaMantos.Admin.API.Domain.Exceptions;

namespace backend.Domain.Entities
{
    public class Loja : EntidadeBase
    {
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
            if(string.IsNullOrWhiteSpace(nome))
                throw new DomainException("O nome da loja é obrigatório");

            if(string.IsNullOrWhiteSpace(site))
                throw new DomainException("O site da loja é obrigatório");

            if(!UrlUtils.UrlValida(site))
                throw new DomainException("O site da loja é inválido");

            if (string.IsNullOrWhiteSpace(urlBusca))
                throw new DomainException("A URL de busca da loja é obrigatória");

            if(!UrlUtils.UrlValida(urlBusca))
                throw new DomainException("A URL de busca da loja é inválida");

            this.Id = id;
            this.Nome = nome;
            this.Site = site;
            this.UrlBusca = urlBusca;
            this.Parceira = parceira;
            this.Ativa = ativa;
            this.Times = times ?? new List<Time>();
        }

        public void AlterarTimes(IList<Time> times)
        {
            if (times == null || !times.Any())
                throw new DomainException("A loja precisa ter pelo menos um time com que trabalha");
            if (times.GroupBy(t => t.Id).Any(g => g.Count() > 1))
                throw new DomainException("Há times duplicados na lista de times da loja");

            this.Times = times ?? this.Times;
        }        
    }
}
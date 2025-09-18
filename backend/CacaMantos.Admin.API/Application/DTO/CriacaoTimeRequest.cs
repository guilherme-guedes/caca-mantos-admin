using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CacaMantos.Admin.API.Application.DTO
{
    public class CriacaoTimeRequest
    {        
        public String Nome { get; private set; }
        public String Identificador { get; private set; }
        public String NomeBusca { get; private set; }
        public IList<String> Termos { get; private set; }
        public bool Destaque { get; private set; }
        public bool Ativo { get; private set; }
        public bool Principal { get; private set; }
        public IList<String> Homonimos { get; private set; }
        public String TimePrincipal { get; private set; }
    }
}
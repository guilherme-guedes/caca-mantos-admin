namespace CacaMantos.Admin.API.Infra.Data.Model
{
    public class TimeModel : EntityBaseModel
    {
        public String Nome { get; set; }
        public String Identificador { get; set; }
        public String NomeBusca { get; set; }
        public IList<String> Termos { get; set; }
        public bool Destaque { get; set; }
        public bool Ativo { get; set; }
        public bool Principal { get; set; }
        public IList<TimeModel> Homonimos { get; set; }
        public virtual IList<LojaTimeModel> Lojas { get; set; }
        public Guid? IdTimePrincipal { get; set; }
        public virtual TimeModel TimePrincipal { get; set; }
    }
}

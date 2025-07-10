namespace backend.Infra.Data.Postgre.Model
{
    public class TimeModel : EntityModel
    {        
        public String nome { get; set; }
        public String identificador { get; set; }
        public String nomeBusca { get; set; }
        public IList<String> termos { get; set; }
        public bool destaque { get; set; }
        public bool ativo { get; set; }
        public bool principal { get; set; }
        public IList<TimeModel> homonimos { get; set; }
        public IList<LojaTimeModel> lojas { get; set; }
        public Guid? timePrincipalId { get; set; }
        public TimeModel timePrincipal { get; set; }
    }
}
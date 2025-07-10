namespace backend.Infra.Data.Postgre.Model
{
    public class LojaModel : EntityModel
    {        
        public String nome { get; set; }
        public String site { get; set; }
        public String urlBusca { get; set; }
        public bool parceira { get; set; }
        public bool ativa { get; set; }
        public IList<LojaTimeModel> times { get; set; }
    }
}
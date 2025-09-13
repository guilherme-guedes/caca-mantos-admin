namespace backend.Infra.Data.Model
{
    public class LojaModel : EntityBaseModel
    {        
        public String nome { get; set; }
        public String site { get; set; }
        public String urlBusca { get; set; }
        public bool parceira { get; set; }
        public bool ativa { get; set; }
        public IList<LojaTimeModel> times { get; set; }
    }
}
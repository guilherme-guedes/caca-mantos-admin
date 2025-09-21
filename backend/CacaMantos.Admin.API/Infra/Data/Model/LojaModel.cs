namespace CacaMantos.Admin.API.Infra.Data.Model
{
    public class LojaModel : EntityBaseModel
    {
        public String Nome { get; set; }
        public String Site { get; set; }
        public String UrlBusca { get; set; }
        public bool Parceira { get; set; }
        public bool Ativa { get; set; }
        public IList<LojaTimeModel> Times { get; set; }
    }
}

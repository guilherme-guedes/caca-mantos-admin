namespace CacaMantos.Admin.API.Infra.Data.Model
{
    public class LojaTimeModel : EntityBaseModel
    {
        public Guid IdLoja { get; set; }
        public LojaModel Loja { get; set; }
        public Guid IdTime { get; set; }
        public TimeModel Time { get; set; }
    }
}

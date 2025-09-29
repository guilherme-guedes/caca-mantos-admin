namespace CacaMantos.Admin.API.Infra.Data.Model
{
    public class LojaTimeModel : EntityBaseModel
    {
        public Guid IdLoja { get; set; }
        public virtual  LojaModel Loja { get; set; }
        public Guid IdTime { get; set; }
        public virtual TimeModel Time { get; set; }
    }
}

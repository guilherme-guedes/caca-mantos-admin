namespace backend.Infra.Data.Model
{
    public class LojaTimeModel : EntityBaseModel
    {
        public Guid idLoja { get; set; }
        public LojaModel loja { get; set; }
        public Guid idTime { get; set; }
        public TimeModel time { get; set; }        
    }
}
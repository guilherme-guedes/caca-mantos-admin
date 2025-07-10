namespace backend.Infra.Data.Postgre.Model
{
    public class LojaTimeModel : EntityModel
    {
        public Guid idLoja { get; set; }
        public LojaModel loja { get; set; }
        public Guid idTime { get; set; }
        public TimeModel time { get; set; }        
    }
}
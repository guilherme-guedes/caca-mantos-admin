namespace CacaMantos.Admin.API.Infra.Data.Repositories
{
    public abstract class BaseRepository
    {
        protected ContextoBanco Context { get; private set; }

        protected BaseRepository(ContextoBanco context)
        {
            this.Context = context ?? throw new ArgumentNullException(nameof(context), "Contexto do banco não pode ser nulo.");
        }
    }
}

namespace backend.Infra.Data.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly ContextoBanco _context;

        public BaseRepository(ContextoBanco context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context), "Contexto do banco não pode ser nulo.");
        }
    }
}
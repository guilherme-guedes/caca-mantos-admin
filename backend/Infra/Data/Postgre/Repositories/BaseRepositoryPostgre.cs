namespace backend.Infra.Data.Postgre.Repositories
{
    public abstract class BaseRepositoryPostgre
    {
        protected readonly ContextoBancoPostgres _context;

        public BaseRepositoryPostgre(ContextoBancoPostgres context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context), "Contexto do banco não pode ser nulo.");
        }
    }
}
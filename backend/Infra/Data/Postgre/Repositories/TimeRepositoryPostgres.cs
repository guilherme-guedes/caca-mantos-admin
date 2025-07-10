using backend.Domain.IRepositories;
using backend.Domain.Model;
using backend.DTO;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace backend.Infra.Data.Postgre.Repositories
{
    public class TimeRepositoryPostgres  : BaseRepositoryPostgre, ITimeRepository
    {
        public TimeRepositoryPostgres(ContextoBancoPostgres context) : base(context)
        {
        }

        public Task<Time> Criar(Time time)
        {
            throw new NotImplementedException();
        }

        public Task<Time> Atualizar(Time time)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Excluir(Guid id)
        {
            var timeModel = await _context.Times.FirstOrDefaultAsync(l => l.id == id);
            if (timeModel is null)
                throw new KeyNotFoundException($"Time com ID {id} não encontrado.");

            var removido = _context.Times.Remove(timeModel) is not null;
            _context.SaveChanges();
            return removido;
        }

        public async Task<Time> Obter(Guid id)
        {
            var timeModel = await _context.Times
                                    .Include(l => l.homonimos)
                                    .AsNoTracking().FirstOrDefaultAsync(l => l.id == id);
            if (timeModel is null)
                throw new KeyNotFoundException($"Time com ID {id} não encontrado.");

            return timeModel.Adapt<Time>();
        }

        public async Task<PaginaDTO<Time>> Consultar(int pagina = 1,
                                            int tamanhoPagina = 5,
                                            string trecho = null,
                                            bool? destaque = null,
                                            bool? ativo = null,
                                            bool? principal = null)
        {
            var query = _context.Times.AsQueryable();

            if (!string.IsNullOrEmpty(trecho))
            {
                trecho = trecho.ToLowerInvariant();
                query = query.Where(t => t.nome.ToLowerInvariant().Contains(trecho) || t.identificador.Contains(trecho));
            }

            if (destaque.HasValue)
                query = query.Where(t => t.destaque == destaque.Value);

            if (ativo.HasValue)
                query = query.Where(t => t.ativo == ativo.Value);

            if (principal.HasValue)
                query = query.Where(t => t.principal == principal.Value);

            var timeModels = await query.AsNoTracking()
                                .Skip((pagina - 1) * tamanhoPagina)
                                .Take(tamanhoPagina)
                                .ToListAsync();
                                
            var totalRegistros = query.Count();

            var times = timeModels.Adapt<List<Time>>();

            return new PaginaDTO<Time>(pagina, tamanhoPagina, totalRegistros, times);
        }

    }
}
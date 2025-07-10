using backend.Domain.IRepositories;
using backend.Domain.Model;
using backend.DTO;
using backend.Infra.Data.Postgre.Model;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace backend.Infra.Data.Postgre.Repositories
{
    public class LojaRepositoryPostgres : BaseRepositoryPostgre, ILojaRepository
    {
        public LojaRepositoryPostgres(ContextoBancoPostgres context) : base(context)
        {
        }


        public Task<Loja> Criar(Loja loja)
        {
            throw new NotImplementedException();
        }

        public Task<Loja> Atualizar(Loja loja)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Excluir(Guid id)
        {
            var lojaModel = await _context.Lojas.FirstOrDefaultAsync(l => l.id == id);
            if (lojaModel is null)
                throw new KeyNotFoundException($"Loja com ID {id} não encontrada.");

            var removida = _context.Lojas.Remove(lojaModel) is not null;
            _context.SaveChanges();
            return removida;
        }

        public async Task<Loja> Obter(Guid id)
        {
            var lojaModel = await _context.Lojas
                                    .Include(l => l.times)
                                    .Include("times.time")
                                    .AsNoTracking().FirstOrDefaultAsync(l => l.id == id);
            if (lojaModel is null)
                throw new KeyNotFoundException($"Loja com ID {id} não encontrada.");

            return lojaModel.Adapt<Loja>();
        }
        
        public async Task<PaginaDTO<Loja>> Consultar(int pagina = 1,
                                            int tamanhoPagina = 5,
                                            string trecho = null,
                                            bool? parceira = null,
                                            bool? ativo = null)
        {
            var query = _context.Lojas.AsQueryable();

            if (!string.IsNullOrEmpty(trecho))
            {
                trecho = trecho.ToLowerInvariant();
                query = query.Where(l => l.nome.ToLowerInvariant().Contains(trecho) || l.site.Contains(trecho));
            }

            if (parceira.HasValue)
                query = query.Where(l => l.parceira == parceira.Value);

            if (ativo.HasValue)
                query = query.Where(l => l.ativa == ativo.Value);

            var lojaModels = await query.AsNoTracking()
                                .Skip((pagina - 1) * tamanhoPagina)
                                .Take(tamanhoPagina)
                                .ToListAsync();
                                
            var totalRegistros = query.Count();

            var lojas = lojaModels.Adapt<List<Loja>>();

            return new PaginaDTO<Loja>(pagina, tamanhoPagina, totalRegistros, lojas);
        }
    }
}
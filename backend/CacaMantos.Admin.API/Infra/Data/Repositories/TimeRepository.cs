using backend.Domain.IRepositories;
using backend.Domain.Entities;
using backend.Domain.Pesquisas;
using backend.Common.DTO;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace backend.Infra.Data.Repositories
{
    public class TimeRepository  : BaseRepository, ITimeRepository
    {
        public TimeRepository(ContextoBanco context) : base(context)
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

        public async Task<PaginaDTO<Time>> Consultar(PesquisaPaginadaTime pesquisa)
        {
            if(pesquisa is null)
                return PaginaDTO<Time>.Vazia(1, 5);

            var query = _context.Times.AsQueryable();

            if (pesquisa.TemTrechoInformado())
            {
                var trechoLike = $"%{pesquisa.Trecho.ToLowerInvariant()}%";
                query = query.Where(t =>
                    EF.Functions.ILike(t.nome, trechoLike) ||
                    EF.Functions.ILike(t.identificador, trechoLike)
                );
            }

            if (pesquisa.TemDestaqueInformado())
                query = query.Where(t => t.destaque == pesquisa.Destaque.Value);

            if (pesquisa.TemAtivoInformado())
                query = query.Where(t => t.ativo == pesquisa.Ativo.Value);

            if (pesquisa.TemPrincipalInformado())
                query = query.Where(t => t.principal == pesquisa.Principal.Value);

            var timeModels = await query.AsNoTracking()
                                .Skip((pesquisa.Pagina - 1) * pesquisa.TamanhoPagina)
                                .Take(pesquisa.TamanhoPagina)
                                .OrderBy(t => t.nome)
                                .ToListAsync();
                                
            var totalRegistros = query.Count();

            var times = timeModels.Adapt<List<Time>>();

            return new PaginaDTO<Time>(pesquisa.Pagina, pesquisa.TamanhoPagina, totalRegistros, times);
        }
    }
}
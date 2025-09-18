using backend.Domain.IRepositories;
using backend.Domain.Entities;
using backend.Domain.Pesquisas;
using backend.Common.DTO;
using Mapster;
using Microsoft.EntityFrameworkCore;
using backend.Infra.Data.Model;
using CacaMantos.Admin.API.Infra.Data.Helper;

namespace backend.Infra.Data.Repositories
{
    public class TimeRepository : BaseRepository, ITimeRepository
    {
        private readonly IRepositorioUtils utils;

        public TimeRepository(ContextoBanco context, IRepositorioUtils utils) : base(context)
        {
            this.utils = utils;
        }

        public async Task<Time> Criar(Time time)
        {
            if (time == null)
                throw new ArgumentNullException(nameof(time));

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var timeModel = time.Adapt<TimeModel>();

                // if (time.TemTimePrincipal())
                //     timeModel.timePrincipalId = time.TimePrincipal.Id;

                await _context.Times.AddAsync(timeModel);
                await _context.SaveChangesAsync();

                if (time.TemTimesHomonimos())
                {
                    var timesHomonimos = _context.Times.Where(t => time.Homonimos.Select(th => th.Id).Contains(t.id)).ToList();
                    timesHomonimos.ForEach(th => th.timePrincipalId = time.Id);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return await ConsultarDadosTimeParaRetorno(time);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<Time> Atualizar(Time time)
        {
            if (time == null)
                throw new ArgumentNullException(nameof(time));

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var timeModel = await _context.Times
                    .Include(t => t.homonimos)
                    .FirstOrDefaultAsync(t => t.id == time.Id);

                if (timeModel == null)
                    throw new KeyNotFoundException($"Time com ID {time.Id} não encontrado.");

                time.Adapt(timeModel);

                // if (time.TemTimePrincipal())
                //     timeModel.timePrincipalId = time.TimePrincipal.Id;

                _context.Times.Update(timeModel);

                if (time.TemTimesHomonimos())
                {
                    var timesHomonimos = _context.Times.Where(t => time.Homonimos.Select(th => th.Id).Contains(t.id)).ToList();
                    timesHomonimos.ForEach(th => th.timePrincipalId = time.Id);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return await ConsultarDadosTimeParaRetorno(time);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
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
            if (pesquisa is null)
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

            var queryPaginada = query.AsNoTracking()
                                .OrderBy(t => t.nome)
                                .Skip((pesquisa.Pagina - 1) * pesquisa.TamanhoPagina)
                                .Take(pesquisa.TamanhoPagina);

            var timesModel = await queryPaginada.ToListAsync();
            var totalRegistros = await query.CountAsync();

            var times = timesModel.Adapt<List<Time>>();

            return new PaginaDTO<Time>(pesquisa.Pagina, pesquisa.TamanhoPagina, totalRegistros, times);
        }

        public Task<int> ObterQuantidadeTimes()
        {
            return _context.Times.CountAsync();
        }

        public async Task<List<Time>> Consultar(IList<Guid> ids)
        {
            var times = await this.utils.CarregarDadosDeIds(_context.Times, ids);
            return times.Adapt<List<Time>>();
        }

        private async Task<Time> ConsultarDadosTimeParaRetorno(Time time)
        {
            var timeModelComHomonimos = await _context.Times
                                .Include(t => t.homonimos)
                                .AsNoTracking()
                                .FirstOrDefaultAsync(t => t.id == time.Id);

            return timeModelComHomonimos.Adapt<Time>();
        }
    }
}
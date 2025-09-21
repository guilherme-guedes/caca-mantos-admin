using CacaMantos.Admin.API.Common.DTO;
using CacaMantos.Admin.API.Domain.Entities;
using CacaMantos.Admin.API.Domain.IRepositories;
using CacaMantos.Admin.API.Domain.Pesquisas;
using CacaMantos.Admin.API.Infra.Data.Helper;
using CacaMantos.Admin.API.Infra.Data.Model;

using Mapster;

using Microsoft.EntityFrameworkCore;

namespace CacaMantos.Admin.API.Infra.Data.Repositories
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
            ArgumentNullException.ThrowIfNull(time);

            using var transaction = await Context.Database.BeginTransactionAsync().ConfigureAwait(false);

            try
            {
                var timeModel = time.Adapt<TimeModel>();

                if (time.TemTimePrincipal())
                    timeModel.TimePrincipalId = time.TimePrincipal.Id;

                await Context.Times.AddAsync(timeModel).ConfigureAwait(false);
                await Context.SaveChangesAsync().ConfigureAwait(false);

                if (time.TemTimesHomonimos())
                {
                    var timesHomonimos = Context.Times.Where(t => time.Homonimos.Select(th => th.Id).Contains(t.Id)).ToList();
                    timesHomonimos.ForEach(th => th.TimePrincipalId = time.Id);
                }

                await Context.SaveChangesAsync().ConfigureAwait(false);
                await transaction.CommitAsync().ConfigureAwait(false);

                return await ConsultarDadosTimeParaRetorno(time).ConfigureAwait(false);
            }
            catch
            {
                await transaction.RollbackAsync().ConfigureAwait(false);
                throw;
            }
        }

        public async Task<Time> Atualizar(Time time)
        {
            ArgumentNullException.ThrowIfNull(time);

            using var transaction = await Context.Database.BeginTransactionAsync().ConfigureAwait(false);

            try
            {
                var timeModel = await Context.Times
                    .Include(t => t.Homonimos)
                    .Include(l => l.TimePrincipal)
                    .FirstOrDefaultAsync(t => t.Id == time.Id)
                    .ConfigureAwait(false);

                if (timeModel == null)
                    throw new KeyNotFoundException($"Time com ID {time.Id} não encontrado.");

                time.Adapt(timeModel);

                if (time.TemTimePrincipal())
                    timeModel.TimePrincipalId = time.TimePrincipal.Id;

                Context.Times.Update(timeModel);

                if (time.TemTimesHomonimos())
                {
                    var timesHomonimos = Context.Times.Where(t => time.Homonimos.Select(th => th.Id).Contains(t.Id)).ToList();
                    timesHomonimos.ForEach(th => th.TimePrincipalId = time.Id);
                }

                await Context.SaveChangesAsync().ConfigureAwait(false);
                await transaction.CommitAsync().ConfigureAwait(false);

                return await ConsultarDadosTimeParaRetorno(time).ConfigureAwait(false);
            }
            catch
            {
                await transaction.RollbackAsync().ConfigureAwait(false);
                throw;
            }
        }

        public async Task<bool> Excluir(Guid id)
        {
            var timeModel = await Context.Times.FirstOrDefaultAsync(l => l.Id == id).ConfigureAwait(false);
            if (timeModel is null)
                throw new KeyNotFoundException($"Time com ID {id} não encontrado.");

            var removido = Context.Times.Remove(timeModel) is not null;
            await Context.SaveChangesAsync().ConfigureAwait(false);
            return removido;
        }

        public async Task<Time> Obter(Guid id)
        {
            var timeModel = await Context.Times
                                    .Include(l => l.Homonimos)
                                    .Include(l => l.TimePrincipal)
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(l => l.Id == id)
                                    .ConfigureAwait(false);
            if (timeModel is null)
                throw new KeyNotFoundException($"Time com ID {id} não encontrado.");

            return timeModel.Adapt<Time>();
        }

        public async Task<PaginaDTO<Time>> Consultar(PesquisaPaginadaTime pesquisa)
        {
            if (pesquisa is null)
                return PaginaDTO<Time>.Vazia(1, 5);

            var query = Context.Times.AsQueryable();

            if (pesquisa.TemTrechoInformado())
            {
                var trechoLike = $"%{pesquisa.Trecho}%";
                query = query.Where(t =>
                    EF.Functions.ILike(t.Nome, trechoLike) ||
                    EF.Functions.ILike(t.Identificador, trechoLike)
                );
            }

            if (pesquisa.TemDestaqueInformado())
                query = query.Where(t => t.Destaque == pesquisa.Destaque.Value);

            if (pesquisa.TemAtivoInformado())
                query = query.Where(t => t.Ativo == pesquisa.Ativo.Value);

            if (pesquisa.TemPrincipalInformado())
                query = query.Where(t => t.Principal == pesquisa.Principal.Value);

            var queryPaginada = query.AsNoTracking()
                                .OrderBy(t => t.Nome)
                                .Skip((pesquisa.Pagina - 1) * pesquisa.TamanhoPagina)
                                .Take(pesquisa.TamanhoPagina);

            var timesModel = await queryPaginada.ToListAsync().ConfigureAwait(false);
            var totalRegistros = await query.CountAsync().ConfigureAwait(false);

            var times = timesModel.Adapt<List<Time>>();

            return new PaginaDTO<Time>(pesquisa.Pagina, pesquisa.TamanhoPagina, totalRegistros, times);
        }

        public Task<int> ObterQuantidadeTimes()
        {
            return Context.Times.CountAsync();
        }

        public async Task<List<Time>> Consultar(IList<Guid> ids)
        {
            var times = await utils.CarregarDadosDeIds(Context.Times, ids).ConfigureAwait(false);
            return times.Adapt<List<Time>>();
        }

        private async Task<Time> ConsultarDadosTimeParaRetorno(Time time)
        {
            var timeModelComHomonimos = await Context.Times
                                .Include(t => t.Homonimos)
                                .AsNoTracking()
                                .FirstOrDefaultAsync(t => t.Id == time.Id)
                                .ConfigureAwait(false);

            return timeModelComHomonimos.Adapt<Time>();
        }
    }
}

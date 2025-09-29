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
    public class LojaRepository : BaseRepository, ILojaRepository
    {
        private readonly IRepositorioUtils utils;

        public LojaRepository(ContextoBanco context, IRepositorioUtils utils) : base(context)
        {
            this.utils = utils;
        }

        public async Task<Loja> Criar(Loja loja)
        {
            ArgumentNullException.ThrowIfNull(loja);

            using var transaction = await Context.Database.BeginTransactionAsync().ConfigureAwait(false);

            try
            {
                var lojaModel = loja.Adapt<LojaModel>();
                await Context.Lojas.AddAsync(lojaModel).ConfigureAwait(false);
                await Context.SaveChangesAsync().ConfigureAwait(false);

                if (loja.Times.Any())
                {
                    var timesExistentes = await utils.CarregarDadosDeIds(Context.Times, [.. loja.Times.Select(t => t.Id)]).ConfigureAwait(false);

                    if (timesExistentes.Count != loja.Times.Count)
                        throw new KeyNotFoundException("Um ou mais times informados não foram encontrados.");

                    var lojaTimesModel = loja.Times.Select(t => new LojaTimeModel
                    {
                        IdLoja = lojaModel.Id,
                        IdTime = t.Id
                    }).ToList();

                    await Context.LojasTimes.AddRangeAsync(lojaTimesModel).ConfigureAwait(false);
                }

                await Context.SaveChangesAsync().ConfigureAwait(false);
                await transaction.CommitAsync().ConfigureAwait(false);

                return await ConsultarDadosLojaParaRetorno(lojaModel).ConfigureAwait(false);
            }
            catch
            {
                await transaction.RollbackAsync().ConfigureAwait(false);
                throw;
            }
        }

        public async Task<Loja> Atualizar(Loja loja)
        {
            ArgumentNullException.ThrowIfNull(loja);

            using var transaction = await Context.Database.BeginTransactionAsync().ConfigureAwait(false);

            try
            {
                var lojaExistente = await Context.Lojas.Include(l => l.Times).FirstOrDefaultAsync(l => l.Id == loja.Id).ConfigureAwait(false);

                if (lojaExistente == null)
                    throw new KeyNotFoundException($"Loja com ID {loja.Id} não encontrada.");

                var dadosAtualizados = loja.Adapt<LojaModel>();
                Context.Entry(lojaExistente).CurrentValues.SetValues(dadosAtualizados);

                lojaExistente.Times.Clear();

                if (loja.Times.Any())
                {
                    var timesExistentes = await utils.CarregarDadosDeIds(Context.Times, [.. loja.Times.Select(t => t.Id)]).ConfigureAwait(false);

                    if (timesExistentes.Count != loja.Times.Count)
                        throw new KeyNotFoundException("Um ou mais times informados não foram encontrados.");

                    foreach (var time in timesExistentes)
                    {
                        lojaExistente.Times.Add(new LojaTimeModel
                        {
                            IdLoja = lojaExistente.Id,
                            IdTime = time.Id,
                            Time = time
                        });
                    }
                }

                await Context.SaveChangesAsync().ConfigureAwait(false);
                await transaction.CommitAsync().ConfigureAwait(false);

                return await ConsultarDadosLojaParaRetorno(lojaExistente).ConfigureAwait(false);
            }
            catch
            {
                await transaction.RollbackAsync().ConfigureAwait(false);
                throw;
            }
        }

        public async Task<bool> Excluir(Guid id)
        {
            var lojaModel = await Context.Lojas.FirstOrDefaultAsync(l => l.Id == id).ConfigureAwait(false);
            if (lojaModel is null)
                throw new KeyNotFoundException($"Loja com ID {id} não encontrada.");

            var removida = Context.Lojas.Remove(lojaModel) is not null;
            await Context.SaveChangesAsync().ConfigureAwait(false);
            return removida;
        }

        public async Task<Loja> Obter(Guid id)
        {
            var lojaModel = await Context.Lojas
                                    .Include(l => l.Times)
                                    .ThenInclude(tl => tl.Time)
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(l => l.Id == id).ConfigureAwait(false);
            if (lojaModel is null)
                throw new KeyNotFoundException($"Loja com ID {id} não encontrada.");

            return lojaModel.Adapt<Loja>();
        }

        public async Task<PaginaDTO<Loja>> Consultar(PesquisaPaginadaLoja pesquisa)
        {
            if (pesquisa is null)
                return PaginaDTO<Loja>.Vazia(1, 5);

            var query = Context.Lojas.AsQueryable();

            if (pesquisa.TemTrechoInformado())
            {
                var trechoLike = $"%{pesquisa.Trecho}%";
                query = query.Where(l =>
                    EF.Functions.ILike(l.Nome, trechoLike) ||
                    EF.Functions.ILike(l.Site, trechoLike)
                );
            }

            if (pesquisa.TemParceiraInformado())
                query = query.Where(l => l.Parceira == pesquisa.Parceira.Value);

            if (pesquisa.TemAtivoInformado())
                query = query.Where(l => l.Ativa == pesquisa.Ativo.Value);

            var queryPaginada = query.AsNoTracking()
                                .OrderBy(l => l.Nome)
                                .Skip((pesquisa.Pagina - 1) * pesquisa.TamanhoPagina)
                                .Take(pesquisa.TamanhoPagina);

            var lojasModel = await queryPaginada.ToListAsync().ConfigureAwait(false);
            var totalRegistros = await query.CountAsync().ConfigureAwait(false);

            var lojas = lojasModel.Adapt<List<Loja>>();

            return new PaginaDTO<Loja>(pesquisa.Pagina, pesquisa.TamanhoPagina, totalRegistros, lojas);
        }

        public Task<int> ObterQuantidadeLojas()
        {
            return Context.Lojas.CountAsync();
        }

        private async Task<Loja> ConsultarDadosLojaParaRetorno(LojaModel loja)
        {
            var lojaComTimes = await Context.Lojas
                                    .Include(l => l.Times)
                                    .Include("times.time")
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(l => l.Id == loja.Id)
                                    .ConfigureAwait(false);

            return lojaComTimes.Adapt<Loja>();
        }
    }
}

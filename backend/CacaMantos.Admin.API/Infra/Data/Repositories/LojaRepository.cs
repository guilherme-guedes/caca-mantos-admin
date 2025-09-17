using backend.Domain.IRepositories;
using backend.Domain.Entities;
using backend.Domain.Pesquisas;
using backend.Common.DTO;
using Mapster;
using Microsoft.EntityFrameworkCore;
using backend.Infra.Data.Model;

namespace backend.Infra.Data.Repositories
{
    public class LojaRepository : BaseRepository, ILojaRepository
    {
        public LojaRepository(ContextoBanco context) : base(context)
        {
        }

        public async Task<Loja> Criar(Loja loja)
        {
            if (loja == null)
                throw new ArgumentNullException(nameof(loja));

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var lojaModel = loja.Adapt<LojaModel>();
                await _context.Lojas.AddAsync(lojaModel);
                await _context.SaveChangesAsync();

                if (loja.Times.Any())
                {
                    var lojaTimesModel = loja.Times.Select(t => new LojaTimeModel
                    {
                        idLoja = lojaModel.id,
                        idTime = t.Id
                    }).ToList();

                    await _context.LojasTimes.AddRangeAsync(lojaTimesModel);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return await ConsultarDadosLojaParaRetorno(lojaModel);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<Loja> Atualizar(Loja loja)
        {
            if (loja == null)
                throw new ArgumentNullException(nameof(loja));

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var lojaExistente = await _context.Lojas.Include(l => l.times).FirstOrDefaultAsync(l => l.id == loja.Id);

                if (lojaExistente == null)
                    throw new KeyNotFoundException($"Loja com ID {loja.Id} não encontrada.");

               var dadosAtualizados = loja.Adapt<LojaModel>();
                _context.Entry(lojaExistente).CurrentValues.SetValues(dadosAtualizados);
    
                lojaExistente.times.Clear();                
                if (loja.Times != null && loja.Times.Any())
                {
                    var idsTimes = loja.Times.Select(t => t.Id).ToList();
                    var timesExistentes = await _context.Times.Where(t => idsTimes.Contains(t.id)).ToListAsync();
                        
                    foreach (var time in timesExistentes)
                    {
                        lojaExistente.times.Add(new LojaTimeModel
                        {
                            idLoja = lojaExistente.id,
                            idTime = time.id,
                            time = time
                        });
                    }
                }

                // if (loja.Times.Any())
                // {
                //     var novosTimes = loja.Times.Select(t => new LojaTimeModel
                //     {
                //         idLoja = loja.Id,
                //         idTime = t.Id
                //     }).ToList();

                //     await _context.LojasTimes.AddRangeAsync(novosTimes);
                // }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return await ConsultarDadosLojaParaRetorno(lojaExistente);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> Excluir(Guid id)
        {
            var lojaModel = await _context.Lojas.FirstOrDefaultAsync(l => l.id == id);
            if (lojaModel is null)
                throw new KeyNotFoundException($"Loja com ID {id} não encontrada.");

            var removida = _context.Lojas.Remove(lojaModel) is not null;
            await _context.SaveChangesAsync();
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

        public async Task<PaginaDTO<Loja>> Consultar(PesquisaPaginadaLoja pesquisa)
        {
            if (pesquisa is null)
                return PaginaDTO<Loja>.Vazia(1, 5);

            var query = _context.Lojas.AsQueryable();

            if (pesquisa.TemTrechoInformado())
            {
                var trechoLike = $"%{pesquisa.Trecho.ToLowerInvariant()}%";
                query = query.Where(l =>
                    EF.Functions.ILike(l.nome, trechoLike) ||
                    EF.Functions.ILike(l.site, trechoLike)
                );
            }

            if (pesquisa.TemParceiraInformado())
                query = query.Where(l => l.parceira == pesquisa.Parceira.Value);

            if (pesquisa.TemAtivoInformado())
                query = query.Where(l => l.ativa == pesquisa.Ativo.Value);

            var queryPaginada = query.AsNoTracking()
                                .OrderBy(l => l.nome)
                                .Skip((pesquisa.Pagina - 1) * pesquisa.TamanhoPagina)
                                .Take(pesquisa.TamanhoPagina);

            var lojasModel = await queryPaginada.ToListAsync();
            var totalRegistros = await query.CountAsync();

            var lojas = lojasModel.Adapt<List<Loja>>();

            return new PaginaDTO<Loja>(pesquisa.Pagina, pesquisa.TamanhoPagina, totalRegistros, lojas);
        }

        public Task<int> ObterQuantidadeLojas()
        {
            return _context.Lojas.CountAsync();
        }
        
        private async Task<Loja> ConsultarDadosLojaParaRetorno(LojaModel loja)
        {
            var lojaComTimes = await _context.Lojas
                                    .Include(l => l.times)
                                    .Include("times.time")
                                    .AsNoTracking().FirstOrDefaultAsync(l => l.id == loja.id);

            return lojaComTimes.Adapt<Loja>();
        }
    }
}
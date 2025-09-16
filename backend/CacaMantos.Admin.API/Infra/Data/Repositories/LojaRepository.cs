using backend.Domain.IRepositories;
using backend.Domain.Entities;
using backend.Domain.Pesquisas;
using backend.Common.DTO;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace backend.Infra.Data.Repositories
{
    public class LojaRepository : BaseRepository, ILojaRepository
    {
        public LojaRepository(ContextoBanco context) : base(context)
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
        
        public async Task<PaginaDTO<Loja>> Consultar(PesquisaPaginadaLoja pesquisa)
        {
            if(pesquisa is null)
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

            var lojaModels = await query.AsNoTracking()
                                .OrderBy(l => l.nome)
                                .Skip((pesquisa.Pagina - 1) * pesquisa.TamanhoPagina)
                                .Take(pesquisa.TamanhoPagina)
                                .ToListAsync();
                                
            var totalRegistros = query.Count();

            var lojas = lojaModels.Adapt<List<Loja>>();

            return new PaginaDTO<Loja>(pesquisa.Pagina, pesquisa.TamanhoPagina, totalRegistros, lojas);
        }
    }
}
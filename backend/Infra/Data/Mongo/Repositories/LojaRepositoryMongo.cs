using System.Text.RegularExpressions;
using backend.Domain.IRepositories;
using backend.Domain.Model;
using backend.DTO;
using backend.Infra.Data.Mongo.Documentos;
using backend.Utils;
using Mapster;
using MongoDB.Driver;

namespace backend.Infra.Data.Mongo.Repositories
{
    public class LojaRepositoryMongo : ILojaRepository
    {
        private readonly ILogger<LojaRepositoryMongo> _logger;
        private readonly ContextoBancoMongo  _contexto;

        public LojaRepositoryMongo(ILogger<LojaRepositoryMongo> logger, ContextoBancoMongo contexto)
        {
            _logger = logger;
            _contexto = contexto;
        }

        public async Task<PaginaDTO<Loja>> Consultar(int pagina = 1,
                                            int tamanhoPagina = 5,
                                            string trecho = null,
                                            bool? parceira = null,
                                            bool? ativa = null)
        {
            _logger.LogDebug("Consultando lojas...");

            pagina = pagina <= 0 ? 1 : pagina;
            tamanhoPagina = tamanhoPagina <= 0 || tamanhoPagina > 20 ? 10 : tamanhoPagina;

            var builder = Builders<LojaDocumento>.Filter;
            FilterDefinition<LojaDocumento> filter = builder.Where(x => 1 == 1);
            SortDefinition<LojaDocumento> sort = null;

            if (!String.IsNullOrEmpty(trecho))
            {                
                filter &= builder.Regex("nome", new Regex(StringUtils.SanitizarBusca(trecho), RegexOptions.IgnoreCase));
                filter &= builder.Regex("site", new Regex(StringUtils.SanitizarBusca(trecho), RegexOptions.IgnoreCase));

                sort = Builders<LojaDocumento>.Sort.Descending(bson => bson.Nome);
            }

            if (ativa.HasValue)
                filter &= builder.Where(l => l.Ativa);                
                
            if (parceira.HasValue)
                filter &= builder.Where(l => l.Parceira);               
            
            var skip = (pagina - 1) * tamanhoPagina;
            var query = _contexto.Lojas.Find(filter).Sort(sort);

            var totalTask = query.CountDocumentsAsync();
            var taskDocumentos = query.Skip(skip).Limit(tamanhoPagina).ToListAsync();

            await Task.WhenAll(totalTask, taskDocumentos);

            return new PaginaDTO<Loja>(paginaAtual: pagina,
                itensPorPagina: tamanhoPagina,
                total: (int)totalTask.Result,
                itens: taskDocumentos.Result.Adapt<List<Loja>>());
        }

        public async Task<Loja> Obter(string id)
        {
            _logger.LogDebug("Consultando loja {id}...", id);

            var builder = Builders<LojaDocumento>.Filter;
            FilterDefinition<LojaDocumento> filter = builder.Where(x => x.Id == id);

            var query = await _contexto.Lojas.FindAsync(filter);

            return query.FirstOrDefault().Adapt<Loja>();
        }

        public Task<Loja> Criar(Loja loja)
        {
            throw new NotImplementedException();
        }

        public Task<Loja> Atualizar(Loja loja)
        {
            throw new NotImplementedException();
        }

        public Task<Loja> Excluir(Loja loja)
        {
            throw new NotImplementedException();
        }
    }
}
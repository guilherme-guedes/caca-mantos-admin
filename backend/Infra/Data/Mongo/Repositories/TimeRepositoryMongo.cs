using System.Text.RegularExpressions;
using backend.Domain.IRepositories;
using backend.Domain.Model;
using backend.DTO;
using backend.Infra.Data.Documentos;
using backend.Utils;
using Mapster;
using MongoDB.Driver;

namespace backend.Infra.Data.Mongo.Repositories
{
    public class TimeRepositoryMongo : ITimeRepository
    {
        private readonly ILogger<TimeRepositoryMongo> _logger;
        private readonly ContextoBancoMongo  _contexto;

        public TimeRepositoryMongo(ILogger<TimeRepositoryMongo> logger, ContextoBancoMongo contexto)
        {
            _logger = logger;
            _contexto = contexto;
        }

        public async Task<Time> Atualizar(Time time)
        {
            if (time is null)
                throw new Exception($"Time sem dados informados");

            var documentoAtualizado = time.Adapt<TimeDocumento>();
            documentoAtualizado = await _contexto.Times.FindOneAndReplaceAsync(t => t.Id == time.id.ToString(), documentoAtualizado);

            return documentoAtualizado.Adapt<Time>();
        }

        public async Task<PaginaDTO<Time>> Consultar(int pagina = 1,
                                                int tamanhoPagina = 5,
                                                string trecho = null,
                                                bool? destaque = null,
                                                bool? ativo = null,
                                                bool? principal = null)
        {
            _logger.LogDebug("Consultando times...");

            pagina = pagina <= 0 ? 1 : pagina;
            tamanhoPagina = tamanhoPagina <= 0 || tamanhoPagina > 20 ? 10 : tamanhoPagina;

            var builder = Builders<TimeDocumento>.Filter;
            FilterDefinition<TimeDocumento> filter = builder.Where(x => 1 == 1);
            SortDefinition<TimeDocumento> sort = null;

            if (!String.IsNullOrEmpty(trecho))
            {                
                filter &= builder.Regex("nome", new Regex(StringUtils.SanitizarBusca(trecho), RegexOptions.IgnoreCase));
                filter &= builder.Regex("identificador", new Regex(StringUtils.SanitizarBusca(trecho), RegexOptions.IgnoreCase));
                filter &= builder.Regex("nomeBusca", new Regex(StringUtils.SanitizarBusca(trecho), RegexOptions.IgnoreCase));

                sort = Builders<TimeDocumento>.Sort.Descending(bson => bson.Nome).Ascending(bson => bson.Identificador);
            }

            if (ativo.HasValue)
                filter &= builder.Where(l => l.Ativo == ativo.Value);                
                
            if (destaque.HasValue)
                filter &= builder.Where(l => l.Destaque == destaque.Value);

            if (principal.HasValue)
                filter &= builder.Where(l => l.Principal == principal.Value);         

            var skip = (pagina - 1) * tamanhoPagina;
            var query = _contexto.Times.Find(filter).Sort(sort);

            var totalTask = query.CountDocumentsAsync();
            var taskDocumentos = query.Skip(skip).Limit(tamanhoPagina).ToListAsync();

            await Task.WhenAll(totalTask, taskDocumentos);

            return new PaginaDTO<Time>(paginaAtual: pagina,
                itensPorPagina: tamanhoPagina,
                total: (int)totalTask.Result,
                itens: taskDocumentos.Result.Adapt<List<Time>>());
        }

        public async Task<Time> Criar(Time time)
        {
            if(time is null)
                throw new Exception($"Time sem dados informados");

            var timeExistente = await _contexto.Times.FindAsync(t => t.Nome.ToLowerInvariant() == time.nome.ToLowerInvariant());
            if (timeExistente is not null)
                throw new Exception($"Time com nome {time.nome} já existente");

            timeExistente = await _contexto.Times.FindAsync(t => t.Identificador.ToLowerInvariant() == time.identificador.ToLowerInvariant());
            if (timeExistente is not null)
                throw new Exception($"Time com identificador {time.identificador} já existente");

            var documentoTime = time.Adapt<TimeDocumento>();
            await _contexto.Times.InsertOneAsync(documentoTime);

            return documentoTime.Adapt<Time>();
        }

        public async Task<Boolean> Excluir(Guid id)
        {
            var documento = await _contexto.Times.FindOneAndDeleteAsync(id.ToString());
            return documento is not null;
        }

        public async Task<Time> Obter(Guid id)
        {
            _logger.LogDebug("Consultando time {id}...", id.ToString());

            var builder = Builders<TimeDocumento>.Filter;
            FilterDefinition<TimeDocumento> filter = builder.Where(x => x.Id == id.ToString());

            var query = await _contexto.Times.FindAsync(filter);

            return query.FirstOrDefault().Adapt<Time>();
        }
    }
}
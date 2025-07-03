using MongoDB.Bson.Serialization.Attributes;

namespace backend.Infra.Data.Mongo.Documentos
{
    [BsonIgnoreExtraElements]
    public class LojaDocumento
    {
        [BsonElement("id")]
        public string Id { get; set; }

        [BsonElement("nome")]
        public string Nome { get; set; }

        [BsonElement("site")]
        public string Site { get; set; }

        [BsonElement("urlBusca")]
        public string UrlBusca { get; set; }

        [BsonElement("parceira")]
        public bool Parceira { get; set; }

        [BsonElement("ativa")]
        public bool Ativa { get; set; }

        [BsonElement("times")]
        public List<string> Times { get; set; }

        public LojaDocumento()
        {
            Id = string.Empty;
            Nome = string.Empty;
            Parceira = false;
            Ativa = false;
        }
    
    }
}
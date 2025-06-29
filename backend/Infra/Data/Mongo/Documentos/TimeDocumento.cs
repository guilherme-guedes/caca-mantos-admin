using MongoDB.Bson.Serialization.Attributes;

namespace backend.Infra.Data.Documentos
{
    [BsonIgnoreExtraElements]
    public class TimeDocumento
    {        
        [BsonElement("id")]
        public string Id { get; set; }
        
        [BsonElement("identificador")]
        public string Identificador { get; set; }

        [BsonElement("nome")]
        public string Nome { get; set; }
        
        [BsonElement("nomeBusca")]
        public string NomeBusca { get; set; }

        [BsonElement("termos")]
        public IList<string> Termos { get; set; }

        [BsonElement("destaque")]
        public bool Destaque { get; set; }

        [BsonElement("ativo")]
        public bool Ativo { get; set; }

        [BsonElement("principal")]
        public bool Principal { get; set; }

        [BsonElement("homonimos")]
        public IList<string> Homonimos { get; set; }
    }
}
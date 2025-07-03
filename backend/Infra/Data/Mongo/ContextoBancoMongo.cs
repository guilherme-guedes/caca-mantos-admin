using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Infra.Data.Documentos;
using backend.Infra.Data.Mongo.Documentos;
using MongoDB.Driver;

namespace backend.Infra.Data
{
    public class ContextoBancoMongo
    {
        private readonly IMongoDatabase _database;

        public ContextoBancoMongo()
        {
            var connectionString = Environment.GetEnvironmentVariable("mongo__ConnectionString");
            var databaseName = Environment.GetEnvironmentVariable("mongo__DatabaseName");

            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<TimeDocumento> Times =>
            _database.GetCollection<TimeDocumento>("times");
            
        public IMongoCollection<LojaDocumento> Lojas =>
            _database.GetCollection<LojaDocumento>("lojas");
    }
}
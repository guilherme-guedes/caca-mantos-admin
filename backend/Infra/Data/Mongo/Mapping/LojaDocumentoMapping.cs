using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Domain.Factories;
using backend.Domain.IRepositories;
using backend.Domain.Model;
using backend.Infra.Data.Mongo.Documentos;
using Mapster;

namespace backend.Infra.Data.Mongo.Mapping
{
    public class LojaDocumentoMapping: IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<LojaDocumento, Loja>()
                    .MapWith(src => new Loja(
                        src.Id,
                        src.Nome,
                        src.Site,
                        src.UrlBusca,
                        src.Parceira,
                        src.Ativa,
                        src.Times != null ? src.Times.Select(identificador => TimeFactory.CriarComIdentificador(identificador)).ToList() : null
                    ));
        }    
    }
}
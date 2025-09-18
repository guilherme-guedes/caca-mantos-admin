using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Infra.Data.Model;
using CacaMantos.Admin.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CacaMantos.Admin.API.Infra.Data.Helper
{ 
    public class RepositorioUtils : IRepositorioUtils
    {
        public async Task<List<TModel>> CarregarDadosDeIds<TModel>(DbSet<TModel> dbSetDados,
            IList<Guid> ids,
            int tamanhoParticao = 30) where TModel : EntityBaseModel
        {
            List<TModel> modelos = new List<TModel>();

            if (ids == null || !ids.Any())
                return modelos;

            if (ids.Count < tamanhoParticao)
                modelos = await dbSetDados.AsNoTracking().Where(t => ids.Contains(t.id)).ToListAsync();
            else
            {
                var idsAgrupados = ParticionarIds(ids, tamanhoParticao);
                foreach (var grupoIds in idsAgrupados)
                {
                    var grupoTimes = await dbSetDados.AsNoTracking().Where(t => grupoIds.Contains(t.id)).ToListAsync();
                    modelos.AddRange(grupoTimes);
                }
            }

            return modelos;
        }
        
        private List<List<Guid>> ParticionarIds(IList<Guid> ids, int tamanhoParticao = 30)
        {
            return ids.Select((id, index) => new { id, index })
                        .GroupBy(x => x.index / tamanhoParticao)
                        .Select(g => g.Select(x => x.id).ToList())
                        .ToList();
        }
    }    
}
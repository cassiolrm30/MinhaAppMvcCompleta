using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevIO.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly Contexto contexto;
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(Contexto _contexto)
        {
            contexto = _contexto;
            DbSet = contexto.Set<TEntity>();
        }

        public async Task<IEnumerable<Entity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> ObterPorId(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<List<TEntity>> ObterTodos()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task Adicionar(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }

        public virtual async Task Atualizar(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }

        public virtual async Task Remover(Guid id)
        {
            DbSet.Add(new TEntity { Id = id });
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await contexto.SaveChangesAsync();
        }

        public void Dispose()
        {
            contexto?.Dispose();
        }
    }
}
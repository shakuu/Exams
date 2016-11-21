using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using ExamPrep.Data.Common.Repositories.Contracts;
using DbExam.Models;

namespace ExamPrep.Data.Common.Repositories
{
    public class EfGenericRepository<TEntity> : IRepository<TEntity> where TEntity : class, INameable
    {
        private readonly DbContext efContext;
        private readonly IDbSet<TEntity> dbSet;

        public EfGenericRepository(DbContext efContext)
        {
            if (efContext == null)
            {
                throw new ArgumentNullException(nameof(efContext));
            }

            this.efContext = efContext;
            this.dbSet = efContext.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var entry = this.efContext.Entry(entity);
            entry.State = EntityState.Added;
        }

        public IEnumerable<TEntity> All()
        {
            return this.dbSet.ToList();
        }

        public TEntity Find(object id)
        {
            return this.dbSet.Find(id);
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var entry = this.efContext.Entry(entity);
            entry.State = EntityState.Deleted;
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var entry = this.efContext.Entry(entity);
            entry.State = EntityState.Modified;
        }

        public TEntity Find(string name)
        {
            var entity = this.dbSet.Where(e => e.Name == name).FirstOrDefault();
            if (entity == null)
            {
                entity = this.dbSet.Local.Where(e => e.Name == name).FirstOrDefault();
            }

            return entity;
        }
    }
}

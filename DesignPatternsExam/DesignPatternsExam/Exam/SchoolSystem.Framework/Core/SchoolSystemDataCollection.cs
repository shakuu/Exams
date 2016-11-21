using System;
using System.Collections.Generic;

namespace SchoolSystem.Framework.Core
{
    public class SchoolSystemDataCollection<T> : ISchoolSystemDataCollection<T>
    {
        private readonly IDictionary<int, T> entities;

        public SchoolSystemDataCollection()
        {
            this.entities = new Dictionary<int, T>();
        }

        public void Add(int id, T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            this.entities.Add(id, entity);
        }

        public bool ContainsKey(int id)
        {
            return this.entities.ContainsKey(id);
        }

        public T GetById(int id)
        {
            return this.entities[id];
        }

        public void Remove(int id)
        {
            this.entities.Remove(id);
        }
    }
}

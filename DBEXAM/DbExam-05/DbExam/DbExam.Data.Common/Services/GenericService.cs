using System;
using System.Collections.Generic;

using DbExam.Models;

using ExamPrep.Data.Common.Factories;
using ExamPrep.Data.Common.Repositories.Contracts;
using ExamPrep.Data.Common.Services.Contracts;

namespace DbExam.Data.Common.Services
{
    public class GenericService<TModel> : IService<TModel> where TModel : class, INameable
    {
        protected readonly IRepository<TModel> repository;
        protected readonly IUnitOfWorkFactory uowFactory;
        protected readonly Func<TModel> modelFactory;

        public GenericService(IRepository<TModel> repository, IUnitOfWorkFactory uowFactory, Func<TModel> modelFactory)
        {
            this.repository = repository;
            this.uowFactory = uowFactory;
            this.modelFactory = modelFactory;
        }

        public void Add(TModel entity)
        {
            using (var uow = this.uowFactory.GetEfUnitOfWork())
            {
                this.repository.Add(entity);
                uow.Commit();
            }
        }

        public TModel FindOrCreate(TModel model)
        {
            var entity = this.repository.Find(model.Name);
            if (entity == null)
            {
                entity = model;
            }

            return entity;
        }

        public IEnumerable<TModel> All()
        {
            throw new NotImplementedException();
        }

        public void Delete(TModel instance)
        {
            throw new NotImplementedException();
        }

        public TModel Find(object id)
        {
            throw new NotImplementedException();
        }

        public void Update(TModel instance)
        {
            throw new NotImplementedException();
        }
    }
}

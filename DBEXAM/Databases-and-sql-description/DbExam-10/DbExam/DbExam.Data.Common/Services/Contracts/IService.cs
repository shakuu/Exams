using System.Collections.Generic;

namespace ExamPrep.Data.Common.Services.Contracts
{
    public interface IService<TModel> where TModel : class
    {
        TModel FindOrCreate(TModel model);
        
        TModel Find(object id);

        IEnumerable<TModel> All();

        void Delete(TModel instance);

        void Update(TModel instance);
    }
}

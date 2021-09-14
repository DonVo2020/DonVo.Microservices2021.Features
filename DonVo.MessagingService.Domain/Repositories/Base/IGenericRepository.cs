using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DonVo.MessagingService.Domain.Repositories.Base
{
    public interface IGenericRepository<TModel>
    {
        void Update(TModel model);
        void Delete(TModel model);
        void Delete(string id);
        long Count(Expression<Func<TModel, bool>> filter);
        bool DoesExist(Expression<Func<TModel, bool>> filter);

        List<TModel> GetList();
        TModel GetById(string id);
        TModel Create(TModel model);      
        IEnumerable<TModel> Filter(Expression<Func<TModel, bool>> filter);
        TModel GetFirstOrDefault(Expression<Func<TModel, bool>> filter);
        TModel GetSingleOrDefault(Expression<Func<TModel, bool>> filter);        
    }
}

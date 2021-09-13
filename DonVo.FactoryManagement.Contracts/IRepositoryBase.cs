using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DonVo.FactoryManagement.Contracts
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll();
        Task<IEnumerable<T>> FindAllAsync();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression);
        T Create(T entity);
        List<T> CreateAll(List<T> entityList);
        T Update(T entity);
        T Delete(T entity);
        Task<long> NumOfRecord();
        Task<string> GetUniqueId();
        Task<int> SaveChangesAsync();
        List<T> DeleteAll(List<T> entityList);
        Task<IEnumerable<T>> FindByConditionAsyncNoTracking(Expression<Func<T, bool>> expression);
    }
}

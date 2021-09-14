using DonVo.FactoryManagement.Contracts;
using DonVo.FactoryManagement.Models.DbModels;
using DonVo.FactoryManagement.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DonVo.FactoryManagement.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        public FactoryManagementContext RepositoryContext { get; set; }
        public IUtilService Util { get; set; }

        public RepositoryBase(FactoryManagementContext repositoryContext,
            IUtilService _util)
        {
            this.RepositoryContext = repositoryContext;
            this.Util = _util;
        }

        public async Task<long> NumOfRecord()
        {
            return await RepositoryContext.Set<T>().AsQueryable().CountAsync();
        }

        public async Task<string> GetUniqueId()
        {
            int countOfRows = await RepositoryContext.Set<T>().AsQueryable().CountAsync();
            return typeof(T).Name.ToUpper() + countOfRows;
        }

        public IQueryable<T> FindAll()
        {
            Util.LogInfo("---STARTED FINDING ----" + typeof(T).Name.ToUpper() + "----------");
            return this.RepositoryContext.Set<T>().AsNoTracking();
        }

        public async Task<IEnumerable<T>> FindAllAsync()
        {
            Util.LogInfo("---STARTED FINDING ----" + typeof(T).Name.ToUpper() + "----------");
            return await this.FindAll().ToListAsync();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            Util.LogInfo("---STARTED FINDING ----" + typeof(T).Name.ToUpper() + "----------");
            return this.RepositoryContext.Set<T>().AsNoTracking().Where(expression);// .AsNoTracking();
        }

        public async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression)
        {
            Util.LogInfo("---STARTED FINDING ----" + typeof(T).Name.ToUpper() + "----------");
            Util.LogInfo(expression.ToString());
            return await this.FindByCondition(expression).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindByConditionAsyncNoTracking(Expression<Func<T, bool>> expression)
        {
            Util.LogInfo("---STARTED FINDING ----" + typeof(T).Name.ToUpper() + "----------");
            Util.LogInfo(expression.ToString());
            return await this.FindByCondition(expression).ToListAsync();
        }

        public T Create(T entity)
        {
            Type type = entity.GetType();
            _ = type.GetProperty("CreatedDateTime");
            _ = type.GetProperty("CreatedDateTime");

            //type.GetProperty()
            type.GetProperty("CreatedDateTime").SetValue(entity, DateTime.Now);
            type.GetProperty("UpdatedDateTime").SetValue(entity, DateTime.Now);
            type.GetProperty("Id").SetValue(entity, Guid.NewGuid().ToString());
            type.GetProperty("RowStatus").SetValue(entity, DB_ROW_STATUS.ADDED.ToString());
            this.RepositoryContext.Set<T>().Add(entity);
            Util.LogInfo("-------" + typeof(T).Name.ToUpper() + "-----Added-----");
            return entity;
        }

        public List<T> CreateAll(List<T> entityList)
        {
            for (int i = 0; i < entityList.Count; i++)
            {
                Type type = entityList[i].GetType();
                _ = type.GetProperty("CreatedDateTime");
                _ = type.GetProperty("CreatedDateTime");

                //type.GetProperty()
                type.GetProperty("CreatedDateTime").SetValue(entityList[i], DateTime.Now);
                type.GetProperty("UpdatedDateTime").SetValue(entityList[i], DateTime.Now);
                type.GetProperty("Id").SetValue(entityList[i], Guid.NewGuid().ToString());
                type.GetProperty("RowStatus").SetValue(entityList[i], DB_ROW_STATUS.ADDED.ToString());
                this.RepositoryContext.Set<T>().Add(entityList[i]);
            }
            Util.LogInfo("-------" + typeof(T).Name.ToUpper() + "-----All Added-----");
            return entityList;
        }

        public T Update(T entity)
        {
            Type type = entity.GetType();
            type.GetProperty("UpdatedDateTime").SetValue(entity, DateTime.Now);
            type.GetProperty("RowStatus").SetValue(entity, DB_ROW_STATUS.UPDATED.ToString());
            this.RepositoryContext.Set<T>().Update(entity);
            Util.LogInfo("-------" + typeof(T).Name.ToUpper() + "-----Updated-----");
            return entity;
        }

        public T Delete(T entity)
        {
            Type type = entity.GetType();
            type.GetProperty("UpdatedDateTime").SetValue(entity, DateTime.Now);
            type.GetProperty("RowStatus").SetValue(entity, DB_ROW_STATUS.DELETED.ToString());
            this.RepositoryContext.Set<T>().Update(entity);
            Util.LogInfo("-------" + typeof(T).Name.ToUpper() + "-----Deleted-----");
            return entity;
        }
        public List<T> DeleteAll(List<T> entityList)
        {
            for (int i = 0; i < entityList.Count; i++)
            {
                Type type = entityList[i].GetType();
                type.GetProperty("UpdatedDateTime").SetValue(entityList[i], DateTime.Now);
                type.GetProperty("RowStatus").SetValue(entityList[i], DB_ROW_STATUS.DELETED.ToString());
                this.RepositoryContext.Set<T>().Update(entityList[i]);
                Util.LogInfo("-------" + typeof(T).Name.ToUpper() + "-----Deleted-----");
                //Type type = entityList[i].GetType();

                //PropertyInfo? prop = type.GetProperty("CreatedDateTime");
                //PropertyInfo? prop8 = type.GetProperty("CreatedDateTime");

                ////type.GetProperty()
                //type.GetProperty("CreatedDateTime").SetValue(entityList[i], DateTime.Now);
                //type.GetProperty("UpdatedDateTime").SetValue(entityList[i], DateTime.Now);
                //type.GetProperty("Id").SetValue(entityList[i], Guid.NewGuid().ToString());
                //type.GetProperty("RowStatus").SetValue(entityList[i], DB_ROW_STATUS.ADDED.ToString());
                //this.RepositoryContext.Set<T>().Add(entityList[i]);
            }
            Util.LogInfo("-------" + typeof(T).Name.ToUpper() + "-----All Deleted-----");
            return entityList;
        }
        public async Task<int> SaveChangesAsync()
        {
            Task<int> tas = RepositoryContext.SaveChangesAsync();
            await tas;
            Util.LogInfo("------------Saved Successfully-----");
            return tas.Result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ShopAPI.IRepositories
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);

        void AddRange(List<T> entities);

        void Update(T entity);

        void UpdateRange(List<T> entities);

        void Remove(T entity);

        void RemoveRange(List<T> entities);

        T Get(int id);

        List<T> GetAll();

        List<T> Find(Expression<Func<T, bool>> predicate);

        T FirstOrDefault();

        T LastOrDefault();

        bool Any(Expression<Func<T, bool>> predicate);
    }
}

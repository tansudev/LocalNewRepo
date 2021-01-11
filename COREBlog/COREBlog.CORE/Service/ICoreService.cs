using COREBlog.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;


namespace COREBlog.SERVİCE
{
    public interface ICoreService<T> where T : CoreEntity
    {
        bool Add(T item);
        bool Add(List<T> item);
        bool Update(T item);
        bool Remove(T item);
        bool Remove(Guid id);
        bool RemoveAll(Expression<Func<T,bool>>exp);
        T GetByID(Guid id);
        List<T> GetAcive();
        List<T>GetDefault(Expression <Func<T,bool>>exp);
        List<T> GetAll();
        bool Activate(Guid id);
        bool Any(Expression<Func<T, bool>> exp);
        int Save();
    }
}

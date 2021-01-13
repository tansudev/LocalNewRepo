using COREBlog.CORE.Entity;
using COREBlog.CORE.Service;
using COREBlog.MODEL.Context;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using COREBlog.CORE.Entity.Enums;
using System.Text;
using System.Transactions;
using System.Linq;

namespace COREBlog.SERVICE.Base
{
    public class BaseService<T> : ICoreService<T> where T : CoreEntity
    {
        private readonly BlogContext context;
        public BaseService(BlogContext _context)
        {
            this.context = _context;
        }

        public bool Activate(Guid id)
        {
            T activated = GetByID(id);
            activated.Status = Status.Active;
            return Update(activated);
        }

        public bool Add(T item)
        {
            try
            {
                context.Set<T>().Add(item);
                return Save() > 0;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Add(List<T> item)
        {
            try
            {
                using (TransactionScope  ts = new TransactionScope())
                {
                    context.Set<T>().AddRange(item);
                    ts.Complete();
                    return Save() > 0;
                }
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Any(Expression<Func<T, bool>> exp) => context.Set<T>().Any(exp);

        public List<T> GetAcive() => context.Set<T>().Where(x => x.Status != Status.Deleted).ToList();

        public List<T> GetAll() => context.Set<T>().ToList();

        public T GetByDefault(Expression<Func<T, bool>> exp) => context.Set<T>().FirstOrDefault(exp);

        public T GetByID(Guid id)
        {
            return context.Set<T>().Find(id);
        }

        //public List<T> GetDefault(Expression<Func<T, bool>> exp)
        //{
        //    return context.Set<T>().Where(exp).ToList();
        //}

        public List<T> GetDefault(Expression<Func<T, bool>> exp) => context.Set<T>().Where(exp).ToList();

        public bool Remove(T item)
        {
            item.Status = Status.Deleted;
            return Update(item);
        }

        public bool Remove(Guid id)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    T item = GetByID(id);
                    item.Status = Status.Deleted;
                    ts.Complete();
                    return Update(item);
                         
                }
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool RemoveAll(Expression<Func<T, bool>> exp)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    var collection = GetDefault(exp);
                    int count = 0;

                    foreach (var item in collection)
                    {
                        item.Status = Status.Deleted;
                        bool opResult = Update(item);

                        if (opResult) count++;

                    }

                    if (collection.Count == count) ts.Complete();
                    else return false;                   
                }
                return true;
                
            }
            catch (Exception)
            {

                return false;
            }
        }

        public int Save()
        {
            return context.SaveChanges();
        }

        public bool Update(T item)
        {
            try
            {
                context.Set<T>().Update(item);
                return Save() >0;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public void DetachEntity( T item)
        {
            context.Entry<T>(item).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
        }

    }

}

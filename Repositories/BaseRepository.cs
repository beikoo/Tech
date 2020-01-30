using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly TechDBEntities Context;

        public BaseRepository()
        {
            Context = new TechDBEntities();
        }

        public List<T> GetAll ()
        {
            return Context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return Context.Set<T>().Find(id);
        }

        public void Create (T entity)
        {
            Context.Set<T>().Add(entity);
            Context.SaveChanges();
        }
            
        public void Delete (int id)
        {
            T entity = GetById(id);

            if(entity == null)
            {
                throw new ArgumentException();
            }

            Context.Set<T>().Remove(entity);
            Context.SaveChanges();
        }

        public void Update (T entity, Func<T, bool> findByIDPredecate)
        {
            T itemToEdit = Context.Set<T>().FirstOrDefault(findByIDPredecate); //t => t.ID == entity.ID

            if (itemToEdit == null)
            {
                throw new ArgumentException();
            }
            Context.Entry(itemToEdit).State = System.Data.Entity.EntityState.Detached;
            Context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
